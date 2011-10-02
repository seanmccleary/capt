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