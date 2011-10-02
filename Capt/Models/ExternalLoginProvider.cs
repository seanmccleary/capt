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

namespace Capt.Models
{
	/// <summary>
	/// The external login provider might be someone like Twitter, or Facebook, etc.
	/// </summary>
	public partial class ExternalLoginProvider
	{

		/// <summary>
		/// The ID of the "Generic OpenID" login provider.
		/// </summary>
		public static int GenericOpenID = 1;

		/// <summary>
		/// The ID of Facebook.
		/// </summary>
		public static int Facebook = 2;

		/// <summary>
		/// The ID of Twitter.
		/// </summary>
		public static int Twitter = 3;
	}
}