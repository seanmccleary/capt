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
 * Foobar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface for reading Vote objects from and writing them to the data store.
	/// </summary>
	public interface IVoteRepository
	{
		/// <summary>
		/// Get all Vote objects for a given Caption
		/// </summary>
		/// <param name="captionId">ID of the caption whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByCaptionId(int captionId);

		/// <summary>
		/// Get all Vote objects for a given Picture
		/// </summary>
		/// <param name="pictureId">ID of the Picture whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByPictureId(int pictureId);

		/// <summary>
		/// Get all Vote objects created by a given User
		/// </summary>
		/// <param name="userId">ID of the user whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByUserId(int userId);

		/// <summary>
		/// Save (insert or update) a Vote to the data store
		/// </summary>
		/// <param name="vote">The vote you'd like to save</param>
		void Save(Vote vote);

		/// <summary>
		/// Delete a given vote from the data store
		/// </summary>
		/// <param name="vote">The Vote you'd like to delete</param>
		void Delete(Vote vote);
	}
}
