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