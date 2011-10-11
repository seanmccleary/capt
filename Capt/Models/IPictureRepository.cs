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
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface to get Picture objects into/out of the data store.
	/// </summary>
	public interface IPictureRepository
	{
		/// <summary>
		/// Get all Picture objects.
		/// </summary>
		/// <returns></returns>
		IQueryable<Picture> GetAll();

		/// <summary>
		/// Get a Picture object by its ID
		/// </summary>
		/// <param name="pictureId">The ID of the Picture object you want</param>
		/// <returns></returns>
		Picture GetById(int pictureId);

		Picture GetNextToActivate();

		/// <summary>
		/// Get the next picture after another given picture
		/// </summary>
		/// <param name="picture">The picture for which you want the next picture</param>
		/// <returns></returns>
		Picture GetNext(Picture picture);

		/// <summary>
		/// Get the previous picture before another given picture.
		/// </summary>
		/// <param name="picture">The picture for which you want the previous picture</param>
		/// <returns></returns>
		Picture GetPrevious(Picture picture);

		/// <summary>
		/// Get a picture by its attribution URL (used to see if a picture from Flickr's already been used on the site)
		/// </summary>
		/// <param name="url">The URL for the picture</param>
		/// <returns></returns>
		Picture GetByUrl(string url);

		/// <summary>
		/// Save (insert or update) a picture to the data store
		/// </summary>
		/// <param name="picture">The picture you want to save</param>
		void Save(Picture picture);

		/// <summary>
		/// Delete a picture from the data store.
		/// </summary>
		/// <param name="picture">The picture you want to delete</param>
		void Delete(Picture picture);
	}
}
