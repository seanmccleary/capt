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
using Capt.Services;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller actions for saving votes on pictures and captions via AJAX.
	/// </summary>
	public class VoteController : Controller
    {

		private IPictureService _pictureService;

		/// <summary>
		/// Create an instance of this controller with the services of your choice.
		/// </summary>

		public VoteController(IPictureService pictureService)
		{
			_pictureService = pictureService;
		}

		/// <summary>
		/// Create an instance of this controller with default repositories.
		/// </summary>
		public VoteController() : this(
			new PictureService()
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

			Caption caption = _pictureService.GetCaptionById(captionId, ViewBag.IsAdminStuffShown);
			if (caption == null)
			{
				throw new ApplicationException("Trying to save vote for non-existing caption ID " + captionId);
			}

			User user = Session["User"] as User;
			if (user == null)
			{
				throw new ApplicationException("No user in the session when trying to register a caption vote!");
			}

			return _pictureService.AddVoteForCaption(user, caption, isUpvote);

	
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

			Picture picture = _pictureService.GetPictureById(pictureId, ViewBag.IsAdminStuffShown);
			if (picture == null)
			{
				throw new ApplicationException("Trying to save vote for non-existing caption ID " + pictureId);
			}

			User user = Session["User"] as User;
			if (user == null)
			{
				throw new ApplicationException("No user in the session when trying to register a picture vote!");
			}

			return _pictureService.AddVoteForPicture(user, picture, isUpvote);


		}
    }
}
