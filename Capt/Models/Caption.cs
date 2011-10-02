using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// Represents a caption that a user's given to a Picture.
	/// </summary>
	public partial class Caption
	{

		private List<Vote> _votes;

		/// <summary>
		/// How many upvotes does this caption have?
		/// </summary>
		public int UpVotes {
			get
			{
				return (from v in Votes
						where v.Weight > 0
						select v).Count();
			}
		}

		/// <summary>
		/// How many downvotes does this caption have?
		/// </summary>
 		public int DownVotes {
			get
			{
				return (from v in Votes
						where v.Weight < 0
						select v).Count();
			}
		}

		/// <summary>
		/// The votes for this caption!
		/// </summary>
		public List<Vote> Votes
		{
			get
			{
				if (_votes == null)
				{
					_votes = new List<Vote>();
					foreach (var cv in CaptionVotes)
					{
						_votes.Add(cv.Vote);
					}
				}

				return _votes;
			}
		}

		/// <summary>
		/// The captions score (upvotes - downvotes).
		/// </summary>
		public int Score
		{
			get
			{
				return (from v in Votes
						select v.Weight).Sum();
			}
		}
	}
}