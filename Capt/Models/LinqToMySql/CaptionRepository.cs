using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capt.Models;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write Caption objects to/from the MySQL database.
	/// </summary>
	public class CaptionRepository : Repository, ICaptionRepository
	{
		public Caption GetById(int captionId)
		{
			return (from p in db.Captions
					where p.Id == captionId
					select p).SingleOrDefault();

		}

		public IEnumerable<Caption> GetAll()
		{
			return db.Captions;
		}

		public void Save(Caption caption)
		{
			if (caption.Id == 0)
			{
				db.Events.InsertOnSubmit(caption.Event);
			}
			else
			{
				Caption oldCaption = GetById(caption.Id);

				oldCaption.AdminNote = caption.AdminNote;
				oldCaption.IsAnonymous = caption.IsAnonymous;
				oldCaption.IsVisible = caption.IsVisible;
				oldCaption.Text = caption.Text;
			}

			db.SubmitChanges();

		}

		public void Delete(Caption caption)
		{

			db.CaptionVotes.DeleteAllOnSubmit(caption.CaptionVotes);
			db.Votes.DeleteAllOnSubmit(caption.Votes);
			foreach (var vote in caption.Votes)
			{
				db.Events.DeleteOnSubmit(vote.Event);
			}

			db.Captions.DeleteOnSubmit(caption);
			db.Events.DeleteOnSubmit(caption.Event);
			db.SubmitChanges();

		}
	}
}