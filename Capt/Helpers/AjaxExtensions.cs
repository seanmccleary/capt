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