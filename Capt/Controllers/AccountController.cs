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
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Capt.Models;
using Capt.Services;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller for login-related actions
	/// </summary>
	public class AccountController : MVCBasics.Areas.ExternalAuthentication.Controllers.AccountController
	{

		/// <summary>
		/// The Account service layer we'll be using to save users and oauth tokens.
		/// </summary>
		IAccountService _accountService;

		/// <summary>
		/// The zero-argument constructor will use a default service layer
		/// </summary>
		public AccountController()
			: this(
				new Capt.Services.AccountService()
			)
		{
		}

		/// <summary>
		/// Constructor that lets you specify your own service layer
		/// </summary>
		/// <param name="_captService">The service layer object you'd like to use</param>
		public AccountController(IAccountService accountService) : base()
		{
			_accountService = accountService;
		}

		/// <summary>
		/// Get a user by his External ID. If he's new, create and save him.  Put him in the session.
		/// </summary>
		/// <param name="externalId">The ID used by the external provider</param>
		/// <param name="provider">The type of external login (OpenID, Facebook, Twitter, etc.)</param>
		/// <returns></returns>
		override protected void LogUserIn(string externalId, 
			MVCBasics.Areas.ExternalAuthentication.Models.ExternalLoginProvider provider, 
			MVCBasics.Areas.ExternalAuthentication.Models.OAuthToken oauthToken)
		{
			User user = _accountService.GetOrCreateUser(externalId, (int) provider);

			// Did we get an OAuth token?  If so, let's save that baby.
			if (
				oauthToken != null
				&& !oauthToken.IsSession
				&& user.OAuthTokens.Where(
					t => t.ExternalLoginProviderId == (int) provider
				).Count() == 0
				)
			{
				_accountService.SaveOAuthToken(user, oauthToken.Expires ?? DateTime.MaxValue,
					(int) provider, oauthToken.Token, oauthToken.Secret);
			}

			Session["User"] = user;
			FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
		}

		/// <summary>
		/// Log a user out and redirect him to the home page.
		/// </summary>
		/// <returns></returns>
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			Session.Clear();

			return RedirectToAction("Index", "PictureCaptions");
		}

		/// <summary>
		/// Toggle privileged mode on and off for an admin.
		/// </summary>
		/// <param name="returnUrl"></param>
		/// <returns></returns>
		[Authorize(Roles = "Administrator")]
		public ActionResult TogglePrivilegeMode(string returnUrl)
		{
			Session["IsPrivilegeModeEnabled"] = !(Session["IsPrivilegeModeEnabled"] as bool? == true);

			return Redirect(returnUrl);
		}

		/// <summary>
		/// This unexciting action pretty much just displays the login page.
		/// </summary>
		/// <returns></returns>
		public ActionResult LogOn()
		{
			if (Request.IsAuthenticated)
			{
				// User's already logged in?
				return Redirect("/");
			}

			return View();
		}


	}
}
