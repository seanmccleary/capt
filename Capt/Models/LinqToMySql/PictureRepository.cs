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
using Capt.Models;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write Picture objects to/from the MySQL database.
	/// </summary>
	public class PictureRepository : Repository, IPictureRepository
	{
		public IEnumerable<Picture> GetAll()
		{
			return db.Pictures;
		}

		public IEnumerable<Picture> GetRanked(int start, int take, bool isForAdmin)
		{
			if (isForAdmin)
			{
				return db.Pictures.OrderBy(p => p.Rank).Take(take).Skip(start);
			}
			else
			{
				// For some reason if I put DateTime.UtcNow directly in the linq query,
				// it says member access is denied??
				DateTime now = DateTime.UtcNow;
				return (from p in db.Pictures
						where p.IsVisible
						&& p.Activates < now
						&& !p.IsPrivate
						&& (p.UserId == null || !p.User.IsLocked)
						orderby p.Rank
						select p).Take(take).Skip(start);
			}
		}

		public Picture GetById(int pictureId)
		{
			return (from p in db.Pictures
					where p.Id == pictureId
					select p).SingleOrDefault();
		}

		public Picture GetPrevious(Picture picture)
		{
			return (from p in db.Pictures
					where p.Activates < picture.Activates
					orderby p.Activates descending
					select p).FirstOrDefault();
		}

		public Picture GetNext(Picture picture)
		{
			return (from p in db.Pictures
					where p.Activates > picture.Activates
					orderby p.Activates
					select p).FirstOrDefault();
		}

		public Picture GetByUrl(string url)
		{
			return (from p in db.Pictures
					where p.Url == url
					select p).SingleOrDefault();
		}

		public void Save(Picture picture)
		{
			if (picture.Id == 0)
			{
				db.Events.InsertOnSubmit(picture.Event);
			}
			else
			{
				Picture oldPicture = GetById(picture.Id);

				oldPicture.Activates = picture.Activates;
				oldPicture.AdminNote = picture.AdminNote;
				oldPicture.Attribution = picture.Attribution;
				oldPicture.AttributionUrl = picture.AttributionUrl;
				oldPicture.IsNSFW = picture.IsNSFW;
				oldPicture.IsPrivate = picture.IsPrivate;
				oldPicture.IsVisible = picture.IsVisible;
				oldPicture.LicenseId = picture.LicenseId;
				oldPicture.Url = picture.Url;
				oldPicture.UserId = picture.UserId;
			}

			db.SubmitChanges();
		}

		public void Delete(Picture picture)
		{

			// Now delete this picture's captions & their votes and events
			foreach (var caption in picture.Captions)
			{
				db.CaptionVotes.DeleteAllOnSubmit(caption.CaptionVotes);
				db.Votes.DeleteAllOnSubmit(caption.Votes);
				caption.Votes.ForEach(v => db.Events.DeleteOnSubmit(v.Event));
				db.Captions.DeleteOnSubmit(caption);
				db.Events.DeleteOnSubmit(caption.Event);
			}


			db.PictureVotes.DeleteAllOnSubmit(picture.PictureVotes);
			db.Votes.DeleteAllOnSubmit(picture.Votes);
			// Delete this picture's votes
			picture.Votes.ForEach(v => db.Events.DeleteOnSubmit(v.Event));
			db.Pictures.DeleteOnSubmit(picture);
			db.Events.DeleteOnSubmit(picture.Event);
			db.SubmitChanges();

		}

	}
}