using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// The external login provider might be someone like Twitter, or Facebook, etc.
	/// </summary>
	public partial class ExternalLoginProvider
	{

		/// <summary>
		/// The ID of the "Generic OpenID" login provider.
		/// </summary>
		public static int GenericOpenID = 1;

		/// <summary>
		/// The ID of Facebook.
		/// </summary>
		public static int Facebook = 2;

		/// <summary>
		/// The ID of Twitter.
		/// </summary>
		public static int Twitter = 3;
	}
}