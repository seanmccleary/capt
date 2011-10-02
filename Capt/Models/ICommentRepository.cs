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
using Capt.Models;

namespace Capt.Models
{
	/// <summary>
	/// Repository interface for getting Comments into and out of the data store.
	/// </summary>
	public interface ICommentRepository
	{
		/// <summary>
		/// Get a comment by its ID
		/// </summary>
		/// <param name="captionId">The ID of the comment you're after</param>
		/// <returns></returns>
		IEnumerable<Comment> GetByCaptionId(int captionId);

		/// <summary>
		/// Save (update or insert) a comment into the data store.
		/// </summary>
		/// <param name="comment">The commentr you want to save</param>
		void Save(Comment comment);
	}
}
