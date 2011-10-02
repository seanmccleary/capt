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
 * Foobar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// Some kind of event, like a user being created, or a vote being saved, etc.
	/// </summary>
	public partial class Event
	{

		/// <summary>
		/// Instantiate a new Event object, and pre-fill as much information as possible based on the
		/// event type.
		/// </summary>
		/// <param name="eventTypeId">The ID of the event type to use to pre-fill data.</param>
		public Event(int eventTypeId)
			: this() 
		{
			Datetime = DateTime.UtcNow;
			EventTypeId = eventTypeId;

			if (System.Web.HttpContext.Current != null)
			{
				Host = System.Web.HttpContext.Current.Request.UserHostName;
				IPv4Address = BitConverter.ToInt32(System.Net.IPAddress.Parse(
					System.Web.HttpContext.Current.Request.UserHostAddress).GetAddressBytes().Reverse().ToArray(), 0);
			}
			else
			{
				Host = "Not from web";
				IPv4Address = 0;
			}
		}
	}
}