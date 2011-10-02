using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// A class representing a license a photo may have been released under. (i.e. Creative Commons licenses)
	/// </summary>
	public partial class License
	{
		/// <summary>
		/// The ID of the CC-BY 2.0 license
		/// </summary>
		public static int CC_BY_2 = 1;
				
		/// <summary>
		/// The ID of the CC-BY SA 2.0 license
		/// </summary>
		public static int CC_BYSA_2 = 2;

		/// <summary>
		/// The ID of the "public domain mark" (no known copyright).
		/// </summary>
		public static int PDM = 3;
	}
}