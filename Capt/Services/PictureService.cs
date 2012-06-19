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
using Capt.Models;
using Capt.Models.Repositories;

namespace Capt.Services
{
	public class PictureService : IPictureService
	{

		private ICaptionRepository _captionRepo;

		private ICommentRepository _commentRepo;

		private IPictureRepository _pictureRepo;

		private ILicenseRepository _licenseRepo;

		private IVoteRepository _voteRepo;

		/// <summary>
		/// Instantiate a picture service with the default repositories.
		/// </summary>
		public PictureService()
			: this(
				new Capt.Models.Repositories.LinqToMySql.CaptionRepository(),
				new Capt.Models.Repositories.LinqToMySql.CommentRepository(),
				new Capt.Models.Repositories.LinqToMySql.PictureRepository(),
				new Capt.Models.Repositories.LinqToMySql.LicenseRepository(),
				new Capt.Models.Repositories.LinqToMySql.VoteRepository()
			)
		{
		}

		/// <summary>
		/// Instantiate a picture service and specify which repositories to use
		/// </summary>
		public PictureService(ICaptionRepository captionRepo, ICommentRepository commentRepo, 
			IPictureRepository pictureRepo, ILicenseRepository licenseRepo, IVoteRepository voteRepo)
		{
			_captionRepo = captionRepo;
			_commentRepo = commentRepo;
			_pictureRepo = pictureRepo;
			_licenseRepo = licenseRepo;
			_voteRepo = voteRepo;
		}

		/// <see cref="Capt.Services.IPictureService.GetCaptionById"/>
		public Caption GetCaptionById(int captionId, bool isAdmin)
		{
			var caption = _captionRepo.GetById(captionId);

			if (isAdmin)
			{
				return caption;
			}
			else if (
				caption.Picture.Activates < DateTime.UtcNow 
				&& caption.Picture.IsVisible 
				&& (caption.Picture.User == null || !caption.Picture.User.IsLocked)
				&& caption.IsVisible 
				&& !caption.User.IsLocked
			) {
				return caption;
			}
			else
			{
				return null;
			}
		}

		/// <see cref="Capt.Services.IPictureService.SaveCaption"/>
		public void SaveCaption(Caption caption)
		{
			_captionRepo.Save(caption);
		}

		/// <see cref="Capt.Services.IPictureService.DeleteCaption"/>
		public void DeleteCaption(int captionId)
		{
			DeleteCaption(_captionRepo.GetById(captionId));
		}

		/// <see cref="Capt.Services.IPictureService.DeleteCaption"/>
		public void DeleteCaption(Caption caption)
		{
			_captionRepo.Delete(caption);
		}

		/// <summary>
		/// Filter out the captions that a non-admin shouldn't see
		/// </summary>
		/// <param name="captions"></param>
		/// <returns></returns>
		private IQueryable<Caption> FilterCaptionsForNonAdmin(IQueryable<Caption> captions)
		{
			DateTime now = DateTime.UtcNow;
			return (from caption in captions
							where caption.IsVisible && caption.Picture.IsVisible && caption.Picture.Activates < now
							&& !caption.Picture.IsPrivate 
							&& (caption.Picture.UserId == null || !caption.Picture.User.IsLocked)
							select caption);
		}

		/// <see cref="Capt.Services.IPictureService.GetAllCaptions"/>
		public List<Caption> GetAllCaptions(bool isAdmin, int? limit)
		{
			var captions = _captionRepo.GetAll();
			if (limit != null)
			{
				captions = captions.Take((int) limit);
			}
			

			if (!isAdmin)
			{
				captions = FilterCaptionsForNonAdmin(captions);
			}

			return captions.OrderByDescending(c => c.CreateEventId).ToList();
		}

		/// <see cref="Capt.Services.IPictureService.GetCommentsForCaption"/>
		public List<Comment> GetCommentsForCaption(int captionId, bool isAdmin)
		{
			var comments = _commentRepo.GetByCaptionId(captionId);

			if (!isAdmin)
			{
				comments = from c in comments
						   where c.IsVisible
						   select c;
			}

			return comments.ToList();
		}

		/// <see cref="Capt.Services.IPictureService.SaveComment"/>
		public void SaveCommentForCaption(Comment comment, int captionId)
		{

			if (comment.CaptionComments.Count() == 0)
			{
				CaptionComment captionComment = new CaptionComment()
				{
					CaptionId = captionId
				};

				comment.CaptionComments.Add(captionComment);

			}
			_commentRepo.Save(comment);
		}

		/// <see cref="Capt.Services.IPictureService.GetPictureById"/>
		public Picture GetPictureById(int pictureId, bool isAdmin)
		{
			var pic = _pictureRepo.GetById(pictureId);

			if (isAdmin)
			{
				return pic;
			}
			else if (pic != null && pic.Activates < DateTime.UtcNow && pic.IsVisible && (pic.User == null || !pic.User.IsLocked))
			{
				return pic;
			}
			else
			{
				return null;
			}
		}

		private IQueryable<Picture> FilterPicturesForNonAdmin(IQueryable<Picture> pictures)
		{
			DateTime now = DateTime.UtcNow;
			
			return (from p in pictures
					where p.Activates < now
					&& p.IsVisible && !p.IsPrivate
					&& (!p.UserId.HasValue || !p.User.IsLocked)
					select p);
		}


		/// <see cref="Capt.Services.IPictureService.GetAllPictures"/>
		public List<Picture> GetAllPictures(bool isAdmin)
		{
			var pictures = _pictureRepo.GetAll();

			if (!isAdmin)
			{
				pictures = FilterPicturesForNonAdmin(pictures);
			}

			return pictures.OrderByDescending(p => p.Activates).ToList();
		}

		public List<Picture> GetRankedPictures(int count, int start, bool isAdmin)
		{
			var pictures = _pictureRepo.GetAll().OrderBy(p => p.Rank).Take(count).Skip(start);

			if (!isAdmin)
			{
				pictures = FilterPicturesForNonAdmin(pictures);
			}

			return pictures.ToList();

		}

		/// <see cref="Capt.Services.IPictureService.GetAllPictures"/>
		public Picture GetNextPictureToActivate()
		{
			return _pictureRepo.GetNextToActivate();
		}

		/// <see cref="Capt.Services.IPictureService.GetPreviousPicture"/>
		public Picture GetPreviousPicture(Picture picture)
		{
			return _pictureRepo.GetPrevious(picture);
		}

		/// <see cref="Capt.Services.IPictureService.GetNextPicture"/>
		public Picture GetNextPicture(Picture picture)
		{
			return _pictureRepo.GetNext(picture);
		}

		/// <see cref="Capt.Services.IPictureService.SavePicture"/>
		public void SavePicture(Picture picture)
		{
			_pictureRepo.Save(picture);
		}

		/// <see cref="Capt.Services.IPictureService.DeletePicture"/>
		public void DeletePicture(int pictureId)
		{
			DeletePicture(GetPictureById(pictureId, true));
		}

		/// <see cref="Capt.Services.IPictureService.SavePicture"/>
		public void DeletePicture(Picture picture)
		{
			_pictureRepo.Delete(picture);
		}

		/// <see cref="Capt.Services.IPictureService.GetAllLicenses"/>
		public List<License> GetAllLicenses()
		{
			return _licenseRepo.GetAll().ToList();
		}

		/// <see cref="Capt.Services.IPictureService.AddVoteForPicture"/>
		public int AddVoteForPicture(User user, Picture picture, bool isUpvote)
		{
			if (picture.UserId == user.Id)
			{
				// Someone's trying to vote for his own caption
				return 0;
			}

			// see if this user has voted on this already.
			var votes = _voteRepo.GetByPictureId(picture.Id);

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
				PictureId = picture.Id,
			};

			newVote.PictureVotes.Add(pictureVote);

			_voteRepo.Save(newVote);

			return (from v in votes
					select v.Weight).Sum();
		}

		/// <see cref="Capt.Services.IPictureService.AddVoteForCaptions"/>
		public int AddVoteForCaption(User user, Caption caption, bool isUpvote)
		{

			if (caption.UserId == user.Id)
			{
				// Someone's trying to vote for his own caption
				return 0;
			}

			var votes = _voteRepo.GetByCaptionId(caption.Id);

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
				CaptionId = caption.Id,
			};

			newVote.CaptionVotes.Add(captionVote);

			_voteRepo.Save(newVote);

			return (from v in votes
					select v.Weight).Sum();

		}
	}
}