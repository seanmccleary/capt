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
using Capt.Models.Repositories;

namespace Capt.Models.Repositories.LinqToMySql
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

		public IQueryable<Caption> GetAll()
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