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
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Capt.Models
{
	/// <summary>
	/// Validation attribute to keep people from putting HTML into fields they shouldn't.
	/// (Normally this isn't allowed in ASP.NET by default at all, but fort his application, that
	/// option must be disabled as ADMINS should be able to edit HTML in a form.)
	/// </summary>
	public class NoHtmlAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{

			// If it's an empty string, return true.  The required field validator will nab it.
			if (String.IsNullOrWhiteSpace(value as string))
			{
				return true;
			}

			return !Regex.IsMatch((string) value, "<(.|\n)*?>");

		}
	}
}