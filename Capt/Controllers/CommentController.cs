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
using Capt.Helpers;
using System.Text.RegularExpressions;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller actions for getting and saving comments
	/// </summary>
    public class CommentController : ApplicationController
    {

		private ICommentRepository _commentRepo;

		/// <summary>
		/// Create a new CommentController with the given comment repository
		/// </summary>
		/// <param name="commentRepo"></param>
		public CommentController(ICommentRepository commentRepo)
		{
			_commentRepo = commentRepo;
		}

		/// <summary>
		/// Zero-argument constructor will try and choose the most appropriate repositories for you
		/// </summary>
		public CommentController()
			: this(new Capt.Models.LinqToMySql.CommentRepository())
		{
		}

		/// <summary>
		/// Save a new comment to the database.
		/// </summary>
		/// <param name="captionId">The ID of the caption to which this comment should be attached</param>
		/// <param name="text">The text of the comment itself.</param>
		/// <returns></returns>
		[ValidateInput(false)]
        public string CreateForCaption(int captionId, string text)
        {

			if (!Request.IsAjaxRequest())
			{
				return GetForCaption(captionId);
			}

			// OK JS should have taken care of this but if not ho hum
			text = text.Trim();

			// Strip out any HTML tags the user may have tried to sneak in 
			Regex regHtml = new System.Text.RegularExpressions.Regex("<[^>]*>");
			text = regHtml.Replace(text, "");

			if (text.Length > 140)
			{
				text = text.Substring(0, 140).Trim();
			}

			User user = Session["User"] as User;

			if (user != null)
			{

				Comment newComment = new Comment()
				{
					Event = new Event(EventType.CommentCreated),
					IsVisible = true,
					Text = text,
					UserId = user.Id,
				};

				CaptionComment captionComment = new CaptionComment()
				{
					CaptionId = captionId,
				};

				newComment.CaptionComments.Add(captionComment);
				_commentRepo.Save(newComment);

			}

			return GetForCaption(captionId);
        }

		/// <summary>
		/// Get the comments for a given caption
		/// </summary>
		/// <param name="captionId">The ID of the caption whose comments you want</param>
		/// <returns></returns>
		private string GetForCaption(int captionId)
		{
			var comments = _commentRepo.GetByCaptionId(captionId);

			string list = "<ul class=\"caption_comment_list\">\n";
			foreach (var comment in comments)
			{
				list += "<li>\n" 
					+ "<span class=\"comment_text\">" + comment.Text + "</span>\n"
					+ "<span class=\"comment_timestamp\">" + HtmlExtensions.TimeSince(comment.Event.Datetime) + "</span>\n"
					+ "<span class=\"comment_author\">by " 
						+ (!String.IsNullOrWhiteSpace(comment.User.Name) ? comment.User.Name : "Anonymous")
					+ "</span>\n"
					+ "</li>\n";
			}

			list += "<ul>\n";

			return list;
		}

    }
}
