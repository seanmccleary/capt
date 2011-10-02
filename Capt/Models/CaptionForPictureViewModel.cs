using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// This is a view model that will be used for showing the list of captions under a picture.
	/// </summary>
	public class CaptionForPictureViewModel
	{

		public bool IsUpVotedByViewingUser { get; set; }

		public bool IsDownVotedByViewingUser { get; set; }

		public string Text { get; set; }

		public int Id { get; set; }

		public int Score { get; set; }

		public DateTime Created { get; set; }

		public string Author { get; set; }

		public int AuthorPoints { get; set; }

		public bool IsAnonymous { get; set; }

		public bool IsViewingUserAuthor { get; set; }

		public string AdminNote { get; set; }

		public bool IsVisible { get; set; }

		public bool IsAuthorLocked { get; set; }

		public int AuthorId { get; set; }

		public Picture Picture { get; set; }

		public List<Comment> Comments { get; set; }

		public CaptionForPictureViewModel()
		{
		}

		/// <summary>
		/// Instantiate a CaptionForPictureViewModel, filling in as many default values from the 
		/// specified Caption object as possible.
		/// </summary>
		/// <param name="caption">The caption object from which to take the default values for this</param>
		public CaptionForPictureViewModel(Caption caption) : this() 
		{
			Author = caption.IsAnonymous ? "Anonymous" : caption.User.Name;
			AuthorId = caption.User.Id;
			Created = caption.Event.Datetime;
			Id = caption.Id;
			Text = caption.Text;
			IsDownVotedByViewingUser = false;
			IsUpVotedByViewingUser = false;
			Score = caption.Score;
			IsAnonymous = caption.IsAnonymous;
			IsViewingUserAuthor = false;
			AdminNote = caption.AdminNote;
			IsVisible = caption.IsVisible;
			IsAuthorLocked = caption.User.IsLocked;
			Picture = caption.Picture;
			Comments = new List<Comment>();

			foreach (var cc in caption.CaptionComments)
			{
				Comments.Add(cc.Comment);
			}

			Comments = Comments.OrderBy(c => c.Event.Datetime).ToList();

			AuthorPoints = caption.User.Score;

			if (System.Web.HttpContext.Current.Session["User"] != null)
			{
				User user = System.Web.HttpContext.Current.Session["User"] as User;

				IsUpVotedByViewingUser = (from v in caption.Votes
											  where v.UserId == user.Id
											  && v.Weight > 0
											  select v).Count() > 0;

				IsDownVotedByViewingUser = (from v in caption.Votes
												where v.UserId == user.Id
												&& v.Weight < 0
												select v).Count() > 0;

				IsViewingUserAuthor = (user.Id == caption.UserId);
			}

		}

	}
}