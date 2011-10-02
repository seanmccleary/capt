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