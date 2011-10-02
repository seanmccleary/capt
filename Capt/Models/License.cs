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
using System.Web;

namespace Capt.Models
{
	/// <summary>
	/// A class representing a license a photo may have been released under. (i.e. Creative Commons licenses)
	/// </summary>
	public partial class License
	{
		/// <summary>
		/// The ID of the CC-BY 2.0 license
		/// </summary>
		public static int CC_BY_2 = 1;
				
		/// <summary>
		/// The ID of the CC-BY SA 2.0 license
		/// </summary>
		public static int CC_BYSA_2 = 2;

		/// <summary>
		/// The ID of the "public domain mark" (no known copyright).
		/// </summary>
		public static int PDM = 3;
	}
}