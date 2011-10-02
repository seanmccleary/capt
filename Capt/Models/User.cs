using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	public partial class User
	{

		/// <summary>
		/// The captions score (upvotes - downvotes on this users captions).
		/// </summary>
		public int Score
		{
			get
			{
				int score = 0;
				List<Vote> votesOnUsersCaptions = new List<Vote>();
				foreach (var usersCaption in Captions)
				{
					score += usersCaption.Votes.Sum(v => v.Weight);
				}
				return score;
			}
		}

	}
}