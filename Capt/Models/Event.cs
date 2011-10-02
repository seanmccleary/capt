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