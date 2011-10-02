using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capt.Models
{
	/// <summary>
	/// Pictures, for which users can write captions and on which users can vote.
	/// </summary>
    public partial class Picture
    {

		private List<Vote> _votes;

		/// <summary>
		/// How many upvotes does this picture have
		/// </summary>
		public int UpVotes
		{
			get
			{
				return (from v in Votes
						where v.Weight > 0
						select v).Count();
			}
		}

		/// <summary>
		/// How many downvotes does this picture have
		/// </summary>
		public int DownVotes
		{
			get
			{
				return (from v in Votes
						where v.Weight < 0
						select v).Count();
			}
		}

		/// <summary>
		/// The votes for this picture!
		/// </summary>
		public List<Vote> Votes
		{
			get
			{
				if (_votes == null)
				{
					_votes = new List<Vote>();
					foreach (var cv in PictureVotes)
					{
						_votes.Add(cv.Vote);
					}
				}

				return _votes;
			}
		}

		/// <summary>
		/// The picture's score (upvotes - downvotes).
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
