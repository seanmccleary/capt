using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// Votes can be cast for pictures, or captions.
	/// </summary>
	public partial class Vote
	{

		/// <summary>
		/// Whether or not this vote as an "upvoate" (i.e. if it's weight is greater than 0)
		/// </summary>
		public bool IsUpVote
		{
			get
			{
				return Weight > 0;
			}
		}

	}
}