using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// A class to match a user up with his score, for the scoreboard.
	/// </summary>
	public class RankedUser
	{
		/// <summary>
		/// The user's score
		/// </summary>
		public int Score { get; set; }

		/// <summary>
		/// The user
		/// </summary>
		public User User { get; set; }

		public RankedUser(int score, User user)
		{
			this.Score = score;
			this.User = user;
		}
	}
}