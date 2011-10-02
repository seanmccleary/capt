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