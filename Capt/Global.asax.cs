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
using System.Web.Routing;

namespace Capt
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Picture captions create shortcut", // Route name
				"Picture/{pictureId}", // URL with parameters
				new { controller = "PictureCaptions", action = "Create" } // Parameter defaults
			);

			routes.MapRoute(
				"Picture caption details shortcut", // Route name
				"Caption/{captionId}", // URL with parameters
				new { controller = "PictureCaptions", action = "CaptionDetails" } // Parameter defaults
			);

			routes.MapRoute(
				"New Captions shortcut", // Route name
				"Captions", // URL with parameters
				new { controller = "CaptionForPicture", action = "Index" } // Parameter defaults
			);


			routes.MapRoute(
				"Captions for user shortcut", // Route name
				"User/{userName}", // URL with parameters
				new { controller = "CaptionForPicture", action = "IndexByUser" } // Parameter defaults
			);


			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "PictureCaptions", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);



		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Application_Error()
		{

			var exception = Server.GetLastError();

			if (exception is System.Threading.ThreadAbortException)
			{
				// Sigh... ignore these.
				return;
			}

			var httpException = exception as HttpException;
			Response.Clear();
			Server.ClearError();
			var routeData = new RouteData();
			routeData.Values["controller"] = "Error";
			routeData.Values["action"] = "Default";
			routeData.Values["exception"] = exception;

			Response.StatusCode = 500;
			
			if (httpException != null)
			{
				Response.StatusCode = httpException.GetHttpCode();
				switch (Response.StatusCode)
				{
					case 404:
						routeData.Values["action"] = "Error404";
						break;
				}
			}
		
			IController errorsController = new Capt.Controllers.ErrorController();
			var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
			errorsController.Execute(rc);
			
		}
		
		protected void Session_Start()
		{

			if (Request.IsAuthenticated)
			{
				// Well looks like they're still authenticated, but we don't have a session for them.
				// That most likely happened by someone restarting the server in the middle of their
				// session, using the inProc session handler.
				// Let's just sneak their User object back into the session.
				Capt.Models.IUserRepository userRepo = new Capt.Models.LinqToMySql.UserRepository();
				Capt.Models.User user = userRepo.GetById(Convert.ToInt32(User.Identity.Name));

				if (user == null)
				{
					// Wha?  How in the Sam Heck did this happen?
					// OK.  Let's just de-authenticate the session and pretend like 
					// none of this ever happened.
					System.Web.Security.FormsAuthentication.SignOut();
				}
				else
				{
					Session["User"] = user;
				}
			}

		}

		protected void Application_BeginRequest()
		{

			if( 
				!String.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["ForceDomainName"])
				&& System.Configuration.ConfigurationManager.AppSettings["ForceDomainName"] != Request.Url.Host
				)
			{
				
				Response.Redirect("http://" + System.Configuration.ConfigurationManager.AppSettings["ForceDomainName"]
					+ (!Request.Url.IsDefaultPort ? ":" + Request.Url.Port : "")
					+ Request.Url.PathAndQuery, 
					true);

				

			}



		}
	}
}