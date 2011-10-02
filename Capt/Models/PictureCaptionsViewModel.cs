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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Capt.Models
{
	/// <summary>
	/// This is the model used for the page where a user enters his caption and maybe his name and whether
	/// or not it should be anonymous.  I'm using the IDataErrorInfo interface here for some validation because
	/// we need to ensure that the user entered his name _if_ we don't already know it and _if_ he's not submitting
	/// this anonymously.  Because that has to look at two properties of the class, I can't do it with a simple 
	/// ValidationAttribute.
	/// </summary>
	public class PictureCaptionsViewModel : IDataErrorInfo
	{

		public string AdminNote { get; set; }

		public string AuthorAdminNote { get; set; }

		public bool IsVisible { get; set; }

		public bool IsAuthorLocked { get; set; }

		public bool IsPrivate { get; set; }

		public DateTime Activates { get; set; }

		private bool _isAnonymous = false;

		public int PictureId { get; set; }

		public string PictureUrl { get; set; }

		public DateTime Created { get; set; }

		public string PictureAttribution { get; set; }

		public string PictureAttributionUrl { get; set; }

		public int PictureScore { get; set; }

		public string LicenseLink { get; set; }

		public IEnumerable<CaptionForPictureViewModel> Captions { get; set; }

		private Dictionary<string, string> errors = new Dictionary<string,string>();

		public bool IsVotedUpByViewer { get; set; }

		public bool IsVotedDownByViewer { get; set; }

		public bool IsViewerUploader { get; set; }

		public int CaptionAuthorCount
		{
			get
			{
				return Captions == null ? 0 : Captions.GroupBy(c => c.AuthorId).Count();
			}
		}

		public bool IsAnonymous
		{
			get
			{
				return _isAnonymous;
			}
			set
			{
				_isAnonymous = value;

				if( _isAnonymous ) {
					UserName = "";
				}
			}
		}

		/// <summary>
		/// The default hints to put into the text fields.  Hard-coded like an ace.
		/// </summary>
		private static Dictionary<string, string> defaultTextHints = new Dictionary<string,string>()
		{
			{ "CaptionText", "Your Caption..." },
			{ "UserName",  "Your Nickname..." }

		};

		private static Dictionary<string, string> errorMessages = new Dictionary<string,string>()
		{
			{ "CaptionTextRequired", "You GOTS to enter a caption, dawg!" },
			{ "UserNameRequired", "Where da nickname at, yo?" } 
		};

		[NoHtml(ErrorMessage="It looks like there's some HTML in your caption, and that aian't allowed!")]
		[StringLength(140, ErrorMessage = "Your caption can't be longer than 140 characters. Sorry pal; them's the breaks.")]
		public string CaptionText { get; set; }

		/// <summary>
		/// Whether or not the caption text is currently set to the default hint
		/// </summary>
		public bool IsCaptionTextHinted
		{
			get
			{
				return CaptionText == defaultTextHints["CaptionText"];
			}
		}

		[UserNameMustBeUnique(ErrorMessage = "Someone's already got dibs on that name. (Don't you hate that?)")]
		[StringLength(45, ErrorMessage = "Your nickname can't be longer than 45 characters. (I chose that limit at random.)")]
		[NoHtml(ErrorMessage = "It looks like there's some HTML in your user name, and that aian't allowed!")]
		public string UserName { get; set; }

		/// <summary>
		/// Whether or not the user name is currently set to its default hint
		/// </summary>
		public bool IsUserNameHinted
		{
			get
			{
				return UserName == defaultTextHints["UserName"];
			}
		}
		
		public PictureCaptionsViewModel()
		{
			CaptionText = defaultTextHints["CaptionText"];
			UserName = defaultTextHints["UserName"];
		}

		public PictureCaptionsViewModel(Picture pic, bool isForAdminMode) : this() 
		{
			SetValuesFromPicture(pic, isForAdminMode);
		}

		public void SetValuesFromPicture(Picture pic, bool isForAdminMode)
		{
			PictureUrl = pic.Url;
			PictureAttribution = pic.Attribution;
			PictureId = pic.Id;
			PictureScore = pic.Score;
			PictureAttributionUrl = pic.AttributionUrl;
			Created = pic.Event.Datetime;
			AdminNote = pic.AdminNote;
			LicenseLink = String.Format("<a target=\"_new\" href=\"{0}\"><img src=\"{1}\" /></a>",
				pic.License.InfoUrl, pic.License.ImageUrl);
			IsVisible = pic.IsVisible;
			IsPrivate = pic.IsPrivate;
			Activates = pic.Activates;
			IsAuthorLocked = (pic.User == null ? false : pic.User.IsLocked);
			AuthorAdminNote = (pic.User == null ? String.Empty : pic.User.AdminNote);
			IsViewerUploader = false;
			IsVotedUpByViewer = false;
			IsVotedDownByViewer = false;

			User user = System.Web.HttpContext.Current.Session["User"] as User;
			if (user != null)
			{
				IsViewerUploader = (user.Id == pic.UserId);

				IsVotedUpByViewer = (from v in pic.Votes
									 where v.UserId == user.Id
									 && v.Weight > 0
									 select v).Count() > 0;

				IsVotedDownByViewer = (from v in pic.Votes
									   where v.UserId == user.Id
									   && v.Weight < 0
									   select v).Count() > 0;

			}

			Captions = GetCaptions(pic.Captions, isForAdminMode);
		}

		/// <summary>
		/// Not gonna return any class-level errors here.
		/// </summary>
		public string Error
		{
			get
			{
				return string.Empty;
			}
		}

		public string this[string columnName]
		{
			get
			{
				Validate();
				return errors.ContainsKey(columnName) ? errors[columnName] : String.Empty;
			}
		}

		/// <summary>
		/// Validate the members of this class which rely on one another
		/// TODO: It's really kind of messy.  Clean this up more properly.
		/// </summary>
		private void Validate() {

			errors.Clear();

			// Validate the caption
			if (
				String.IsNullOrWhiteSpace(CaptionText)
				|| CaptionText == defaultTextHints["CaptionText"]
				)
			{
				errors.Add("CaptionText", errorMessages["CaptionTextRequired"]);
			}

			// OK now we're validating the UserName here.
			User user = System.Web.HttpContext.Current.Session["User"] as User;
			if (user == null)
			{
				// oh SCHEISSE
				throw new ApplicationException("We're validating a caption but there's no user in the session?!?!");
			}

			// You know what? Despite what the above code did, we're going to validate the UserName separately down here

			// OK if we don't know the user's name and he hasn't entered it and it's not anonymous, we have a problem.
			if (
				String.IsNullOrWhiteSpace(user.Name)
				&& (String.IsNullOrWhiteSpace(UserName) || UserName == defaultTextHints["UserName"])
				&& !IsAnonymous
				)
			{
				errors.Add("UserName", errorMessages["UserNameRequired"]);
			}
		}

		/// <summary>
		/// Build the collection of the caption view models
		/// </summary>
		/// <param name="captions"></param>
		/// <returns></returns>
		private IEnumerable<CaptionForPictureViewModel> GetCaptions(IEnumerable<Caption> captions, bool isForAdminMode)
		{

			List<CaptionForPictureViewModel> captionsForUser = new List<CaptionForPictureViewModel>();

			// If this isn't for an admin in priviliged mode, then
			// filter out all the captions which are invisible or were created by
			// locked users.
			if (!isForAdminMode)
			{
				captions = from c in captions
							where c.IsVisible && !c.User.IsLocked
							select c;
			}

			foreach (var caption in captions)
			{
				captionsForUser.Add(new CaptionForPictureViewModel(caption));
			}

			string sortOrder = System.Web.HttpContext.Current.Session["captionSortOrder"] as string;
			switch (sortOrder)
			{
				case "oldest":
					return captionsForUser.OrderBy(m => m.Created);

				case "worst":
					return captionsForUser.OrderBy(m => m.Score);

				case "best":
					return captionsForUser.OrderByDescending(m => m.Score);

				default: // Default: Sort by newest 
					return captionsForUser.OrderByDescending(m => m.Created);

			}

		}
	}
}