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

namespace Capt.Controllers
{
	/// <summary>
	/// Controller for the mostly-static general information pages.
	/// </summary>
	[GlobalViewData]
	public class InfoController : Controller
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
