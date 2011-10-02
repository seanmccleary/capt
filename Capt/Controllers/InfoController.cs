using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller for the mostly-static general information pages.
	/// </summary>
    public class InfoController : ApplicationController
    {
        
		/// <summary>
		/// Show the "About this site" page.
		/// </summary>
		/// <returns></returns>
		public ActionResult About()
        {
            return View();
        }

		/// <summary>
		/// Show the "Contact us" page.
		/// </summary>
		/// <returns></returns>
		public ActionResult Contact()
		{
			ViewBag.EmailAddress = System.Configuration.ConfigurationManager.AppSettings["EmailsFrom"];

			return View();
		}

		/// <summary>
		/// Show the terms and conditions page.
		/// </summary>
		/// <returns></returns>
		public ActionResult Conditions()
		{
			return View();
		}

		/// <summary>
		/// Show the privacy policy.
		/// </summary>
		/// <returns></returns>
		public ActionResult Privacy()
		{
			return View();
		}

    }
}
