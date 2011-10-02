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
	/// Repository class to get Caption objects into and out of the data store.
	/// </summary>
	public interface ICaptionRepository
	{
		/// <summary>
		/// Get a Caption object by its ID.
		/// </summary>
		/// <param name="captionId">The ID of the caption you're after.</param>
		/// <returns></returns>
		Caption GetById(int captionId);

		/// <summary>
		/// Save (insert or update) a Caption to the data store.
		/// </summary>
		/// <param name="caption">The caption you're saving</param>
		void Save(Caption caption);

		/// <summary>
		/// Delete a caption from the data store
		/// </summary>
		/// <param name="caption">The caption you want to axe</param>
		void Delete(Caption caption);

		/// <summary>
		/// Get all captions from the data store.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Caption> GetAll();
	}
}
