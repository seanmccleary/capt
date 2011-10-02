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
using System.ComponentModel.DataAnnotations;

namespace Capt.Models
{
	/// <summary>
	/// Validation attribute to ensure no two users have the same user name.
	/// </summary>
	public class UserNameMustBeUniqueAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{

			// If it's an empty string, return true.  The required field validator will nab it.
			if (String.IsNullOrWhiteSpace(value as string))
			{
				return true;
			}

			// See if this user name they've chosen is already taken
			return (
				value != null
				&& (new LinqToMySql.UserRepository()).GetByName(((string)value).Trim()) == null
			);

		}

	}
}