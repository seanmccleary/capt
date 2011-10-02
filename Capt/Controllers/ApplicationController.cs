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
