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