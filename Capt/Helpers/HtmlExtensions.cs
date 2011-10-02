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
using System.Web.Mvc;

namespace Capt.Helpers
{

	/// <summary>
	/// Extra HTML helpers
	/// </summary>
	public static class HtmlExtensions
	{
		/// <summary>
		/// Wrapper for TimeSince(DateTime) so it can be used from view.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="timeStamp"></param>
		/// <returns></returns>
		public static string TimeSince(this HtmlHelper helper, DateTime timeStamp)
		{
			return TimeSince(timeStamp);
		}

		/// <summary>
		/// Take a timestamp and formate it like "X days" or "X hours" or "X minutes" ago
		/// </summary>
		/// <param name="timeStamp"></param>
		/// <returns></returns>
		public static string TimeSince(DateTime timeStamp)
		{
			var delta = DateTime.UtcNow - timeStamp;

			if (delta.Days > 0)
			{
				return (delta.Days == 1 ? "1 day ago" : delta.Days + " days ago");
			}
			else if (delta.Hours > 0)
			{
				return (delta.Hours == 1 ? "1 hour ago" : delta.Hours + " hours ago");
			}
			else if (delta.Minutes > 0)
			{
				return (delta.Minutes == 1 ? "1 minute ago" : delta.Minutes + " minutes ago");
			}
			else
			{
				return (delta.Seconds == 1 ? "1 second ago" : delta.Seconds + " seconds ago");
			}

		}
	}
}