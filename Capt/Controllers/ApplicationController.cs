using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capt.Controllers
{
	/// <summary>
	/// A base class for all the others to inhert, with some basic functionality.
	/// </summary>
    public abstract class ApplicationController : Controller
    {

		/// <summary>
		/// Set up a few view bag variables which should be present for all controllers and actions.
		/// </summary>
		public ApplicationController()
			: base()
		{
			ViewBag.SiteName = System.Configuration.ConfigurationManager.AppSettings["SiteName"];
			ViewBag.IsAdminStuffShown = System.Web.HttpContext.Current.Session["IsPrivilegeModeEnabled"] as bool? == true;

			// TODO: Apparently there's a NuGet package for analytics. Check it out.
			ViewBag.GoogleAnalyticsId = System.Configuration.ConfigurationManager.AppSettings["GoogleAnalyticsId"];

			ViewBag.UseMicrosoftCDN =
				System.Configuration.ConfigurationManager.AppSettings["UseMicrosoftCDN"] != null
				&& System.Configuration.ConfigurationManager.AppSettings["UseMicrosoftCDN"].ToUpper().Trim() == "TRUE";
		}



    }
}
