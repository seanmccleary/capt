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
 * capt is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with capt.  If not, see <http://www.gnu.org/licenses/>.
 */
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
