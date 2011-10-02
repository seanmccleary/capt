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

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write Comment objects to/from the MySQL database.
	/// </summary>
	public class CommentRepository : Repository, ICommentRepository
	{
		public IEnumerable<Comment> GetByCaptionId(int captionId)
		{
			return from c in db.Comments
				   join cc in db.CaptionComments on c.Id equals cc.CommentId
				   where cc.CaptionId == captionId
				   select c;
		}

		public void Save(Comment comment)
		{
			// Make sure this is a new vote we're saving here.  Not too worried about functionality to edit votes.
			if (comment.Id == 0)
			{
				// Smashing.
				db.Events.InsertOnSubmit(comment.Event);
			}
			else
			{
				throw new NotImplementedException("Updating comments isn't implemented yet.");
			}

			db.SubmitChanges();

		}

	}
}