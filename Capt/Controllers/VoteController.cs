/*
 * Copyright 2011 Sean McCleary
 * 
 * This file is part of capt.
 *
 * capt is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * capt is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with capt.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capt.Models;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller actions for saving votes on pictures and captions via AJAX.
	/// </summary>
	[GlobalViewData]
	public class VoteController : Controller
    {

		private IVoteRepository _voteRepo;

		private ICaptionRepository _captionRepo;

		private IPictureRepository _pictureRepo;

		/// <summary>
		/// Create an instance of this controller with the repositories of your choice.
		/// </summary>
		/// <param name="voteRepo"></param>
		/// <param name="captionRepo"></param>
		/// <param name="pictureRepo"></param>
		public VoteController(IVoteRepository voteRepo, ICaptionRepository captionRepo, IPictureRepository pictureRepo)
		{
			_voteRepo = voteRepo;
			_captionRepo = captionRepo;
			_pictureRepo = pictureRepo;
		}

		/// <summary>
		/// Create an instance of this controller with default repositories.
		/// </summary>
		public VoteController() : this(
			new Capt.Models.LinqToMySql.VoteRepository(),
			new Capt.Models.LinqToMySql.CaptionRepository(),
			new Capt.Models.LinqToMySql.PictureRepository()
			)
		{
		}

		/// <summary>
		/// Called via the AJAX to upvote (or downvote) a caption.  What is the proper verb, anyway?  to upvote or to vote up?
		/// </summary>
		/// <param name="captionId">ID of the caption on which the user is voting</param>
		/// <param name="isUpvote">Whether or not this is an upvote</param>
		/// <returns></returns>
		[Authorize]
		public int CreateForCaption(int captionId, bool isUpvote)
		{

			if (!Request.IsAjaxRequest())
			{
				return 0;
			}

			Caption caption = _captionRepo.GetById(captionId);
			if (caption == null)
			{
				throw new ApplicationException("Trying to save vote for non-existing caption ID " + captionId);
			}

			User user = Session["User"] as User;
			if (user == null)
			{
				throw new ApplicationException("No user in the session when trying to register a caption vote!");
			}

			if (caption.UserId == user.Id)
			{
				// Someone's trying to vote for his own caption
				return 0;
			}

			var votes = _voteRepo.GetByCaptionId(captionId);

			// Make sure he ain't a-scammin' us
			if (user.Score < 0)
			{
				// BZZZT!
				return (from v in votes
						select v.Weight).Sum();
			}

			// see if this user has voted on this already.
			
			foreach (Vote vote in votes)
			{
				if (vote.UserId == user.Id)
				{
					// I'll be a son of a gun, he has.  Either he
					// (A) Voted this up before, and is now voting it down, & we should DELETE HIS PREVIOUS VOTE.
					// (B) Voted this down before, and is now voting it up, & we should DELETE HIS PREVIOUS VOTE.
					// (C) Voted this up before, and is voting it up again because there was a screw-up in the JavaScript
					//	or he's a haxx0r, & we should IGNORE THIS VOTE
					// (D) Voted this down before, and is voting it down again becase " " "
					bool isOldUpvote = vote.Weight > 0;

					// Case A or B
					if (
						(isOldUpvote && !isUpvote)
						|| (!isOldUpvote && isUpvote)
					)
					{
						_voteRepo.Delete(vote);
					}

					return (from v in votes
							select v.Weight).Sum();
				}
			}

			// OK let's save his vote I guess.
			Vote newVote = new Vote()
			{
				UserId = user.Id,
				Weight = (isUpvote ? 1 : -1),
				Event = new Event(EventType.VoteCreated) 
			};

			CaptionVote captionVote = new CaptionVote()
			{
				CaptionId = captionId,
			};

			newVote.CaptionVotes.Add(captionVote);

			_voteRepo.Save(newVote);

			return (from v in votes
					select v.Weight).Sum();
		}

		/// <summary>
		/// Called vai the AJAX to upvote (or downvote) a picture.
		/// </summary>
		/// <param name="pictureId">ID of the picture on which the user's voding</param>
		/// <param name="isUpvote">Whether or not this is an upvote</param>
		/// <returns></returns>
		[Authorize]
		public int CreateForPicture(int pictureId, bool isUpvote)
		{
			if (!Request.IsAjaxRequest())
			{
				return 0;
			}

			Picture picture = _pictureRepo.GetById(pictureId);
			if (picture == null)
			{
				throw new ApplicationException("Trying to save vote for non-existing caption ID " + pictureId);
			}

			User user = Session["User"] as User;
			if (user == null)
			{
				throw new ApplicationException("No user in the session when trying to register a picture vote!");
			}

			if (picture.UserId == user.Id)
			{
				// Someone's trying to vote for his own caption
				return 0;
			}

			// see if this user has voted on this already.
			var votes = _voteRepo.GetByPictureId(pictureId);

			// Make sure he ain't a-scammin' us
			if (user.Score < 0)
			{
				// BZZZT!
				return (from v in votes
						select v.Weight).Sum();
			}

			foreach (Vote vote in votes)
			{
				if (vote.UserId == user.Id)
				{
					// I'll be a son of a gun, he has.  Either he
					// (A) Voted this up before, and is now voting it down, & we should DELETE HIS PREVIOUS VOTE.
					// (B) Voted this down before, and is now voting it up, & we should DELETE HIS PREVIOUS VOTE.
					// (C) Voted this up before, and is voting it up again because there was a screw-up in the JavaScript
					//	or he's a haxx0r, & we should IGNORE THIS VOTE
					// (D) Voted this down before, and is voting it down again becase " " "
					bool isOldUpvote = vote.Weight > 0;

					// Case A or B
					if (
						(isOldUpvote && !isUpvote)
						|| (!isOldUpvote && isUpvote)
					)
					{
						_voteRepo.Delete(vote);
					}

					return (from v in votes
							select v.Weight).Sum();
				}
			}

			// OK let's save his vote I guess.
			Vote newVote = new Vote()
			{
				UserId = user.Id,
				Weight = (isUpvote ? 1 : -1),
				Event = new Event(EventType.VoteCreated)
			};

			PictureVote pictureVote = new PictureVote()
			{
				PictureId = pictureId,
			};

			newVote.PictureVotes.Add(pictureVote);

			_voteRepo.Save(newVote);

			return (from v in votes
					select v.Weight).Sum();
		}
    }
}
