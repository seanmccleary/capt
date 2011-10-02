using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write Vote objects to/from the MySQL database.
	/// </summary>
	public class VoteRepository : Repository, IVoteRepository
	{

		public IEnumerable<Vote> GetByCaptionId(int captionId)
		{
			return from v in db.Votes
				   join cv in db.CaptionVotes on v.Id equals cv.VoteId
				   where cv.CaptionId == captionId
				   select v;
		}

		public IEnumerable<Vote> GetByPictureId(int pictureId)
		{
			return from v in db.Votes
					join pv in db.PictureVotes on v.Id equals pv.VoteId
					where pv.PictureId == pictureId
					select v;
		}

		public IEnumerable<Vote> GetByUserId(int userId)
		{
			return from v in db.Votes
					where v.UserId == userId
					select v;
		}

		public void Save(Vote vote)
		{
			// Make sure this is a new vote we're saving here.  Not too worried about functionality to edit votes.
			if (vote.Id == 0)
			{
				// Smashing.
				db.Events.InsertOnSubmit(vote.Event);
			}
			else
			{
				throw new NotImplementedException("Updating votes isn't implemented yet.");
			}

			db.SubmitChanges();
		}

		public void Delete(Vote vote)
		{
			// First we need to delete the entry from the CaptionVote or PictureVote tables.
			db.CaptionVotes.DeleteAllOnSubmit(vote.CaptionVotes);
			db.PictureVotes.DeleteAllOnSubmit(vote.PictureVotes);

			db.Votes.DeleteOnSubmit(vote);
			db.Events.DeleteOnSubmit(vote.Event);

			db.SubmitChanges();
		}

	}
}