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
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Capt.Helpers
{
	/// <summary>
	/// An extra AJAX helper method.
	/// </summary>
	public static class AjaxExtensions
	{

		/// <summary>
		/// Build an image link for an AJAX action. Code taken from comment at 
		/// http://stackoverflow.com/questions/341649/asp-net-mvc-ajax-actionlink-with-image
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="imageUrl"></param>
		/// <param name="altText"></param>
		/// <param name="actionName"></param>
		/// <param name="controller"></param>
		/// <param name="routeValues"></param>
		/// <param name="ajaxOptions"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static MvcHtmlString ActionImageLink(this AjaxHelper helper, string imageUrl, string altText, 
			string actionName, string controller, object routeValues, AjaxOptions ajaxOptions, 
			IDictionary<string, string> htmlAttributes)
		{
			var builder = new TagBuilder("img");
			builder.MergeAttribute("src", imageUrl);
			builder.MergeAttribute("alt", altText);
			builder.MergeAttributes(htmlAttributes);
			var link = helper.ActionLink("[replaceme]", actionName, controller, routeValues, ajaxOptions);
			return new MvcHtmlString(link.ToHtmlString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
		}
	}
}