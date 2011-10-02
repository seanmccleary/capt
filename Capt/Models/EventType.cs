using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// The TYPE of event. 
	/// </summary>
	public partial class EventType
	{
		/// <summary>
		/// This is the ID of the event type when a new user is added to the database.
		/// </summary>
		public static int UserCreated = 1;

		/// <summary>
		/// This is the ID of the event type when a new picture has been added to the database.
		/// </summary>
		public static int PictureCreated = 2;

		/// <summary>
		/// This is the ID of the event type when a new caption has been added to the database.
		/// </summary>
		public static int CaptionCreated = 3;

		/// <summary>
		/// This is the ID of the event type when a new vote has been added to the database.
		/// </summary>
		public static int VoteCreated = 4;

		/// <summary>
		/// This is the ID of the event type when a new external login has been added to the databse.
		/// </summary>
		public static int ExternalLoginCreated = 5;

		/// <summary>
		/// This is the ID of the event type when a new OAuthToken has been added to the database.
		/// </summary>
		public static int OAuthTokenCreated = 6;

		/// <summary>
		/// This is the ID of the event type when a new comment has been added to the database.
		/// </summary>
		public static int CommentCreated = 7;
	}
}