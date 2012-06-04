using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capt.Services;

namespace Capt.Controllers
{
	public class GlobalViewDataAttribute : ActionFilterAttribute
	{

		/// <summary>
		/// The picture service layer to use here
		/// </summary>
		protected IPictureService _pictureService;

		public GlobalViewDataAttribute() :
			this(new PictureService())
		{
			
		}

		/// <summary>
		/// Constructor which allows you to pass in a picture service
		/// </summary>
		/// <param name="pictureService"></param>
		public GlobalViewDataAttribute(IPictureService pictureService) : base()
		{
			_pictureService = pictureService;
		}

		public override void  OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.Controller.ViewData["SiteName"]
				= System.Configuration.ConfigurationManager.AppSettings["SiteName"];
			
			filterContext.Controller.ViewData["IsAdminStuffShown"]
				= System.Web.HttpContext.Current.Session["IsPrivilegeModeEnabled"] as bool? == true;

			// Get the next-to-activeate picture
			var nextPicture = _pictureService.GetNextPictureToActivate();

			filterContext.Controller.ViewData["NextPictureActivates"] = 
				(nextPicture != null ? nextPicture.Activates.ToString() + " UTC-0000" : "");
		}
	}
}