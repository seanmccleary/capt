using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capt.Controllers
{
	public class GlobalViewDataAttribute : ActionFilterAttribute
	{
		public override void  OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.Controller.ViewData["SiteName"]
				= System.Configuration.ConfigurationManager.AppSettings["SiteName"];
			
			filterContext.Controller.ViewData["IsAdminStuffShown"]
				= System.Web.HttpContext.Current.Session["IsPrivilegeModeEnabled"] as bool? == true;
		}
	}
}