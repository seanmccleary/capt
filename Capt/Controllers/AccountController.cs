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
using Twitterizer;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller for login-related actions
	/// </summary>
	public class AccountController : ApplicationController
	{
		/// <summary>
		/// Our service layer!
		/// </summary>
		private IExternalLoginService _captService;

		/// <summary>
		/// The zero-argument constructor will use a default service layer
		/// </summary>
		public AccountController()
			: this(new Capt.Models.LinqToMySql.ExternalLoginService())
		{
		}

		/// <summary>
		/// Constructor that lets you specify your own service layer
		/// </summary>
		/// <param name="_captService">The service layer object you'd like to use</param>
		public AccountController(IExternalLoginService _captService)
		{
			this._captService = _captService;
		}

		/// <summary>
		/// The login form submits to this action.
		/// </summary>
		/// <param name="returnUrl">The URL the user should be sent back to once he's logged in</param>
		/// <returns></returns>
		[ValidateInput(false)]
		public ActionResult Authenticate(string returnUrl)
		{
			try
			{
				string receiveUrl = Url.Action("ReceiveResponse", "Account", null, Request.Url.Scheme);


				// Are we doing OpenID here?
				if (Request.Form["provider_type"] == "openid")
				{
					return Redirect(_captService.GetOpenIdRedirectUrl(Request.Form["openid_identifier"], receiveUrl, returnUrl));
				}

				// How's about Facebook?
				else if (Request.Form["provider_type"] == "fb")
				{
					return Redirect(_captService.GetFacebookRedirectUrl(
						Url.Action("ReceiveFacebookResponse", "Account", null, Request.Url.Scheme)
						, returnUrl));
				}

				// Twitter, perhaps?
				else if (Request.Form["provider_type"] == "twitter")
				{
					string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
					string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

					return Redirect(
						_captService.GetTwitterRedirectUrl(receiveUrl, returnUrl, consumerKey, consumerSecret)
					);
				}

				// That's odd.  How'd we get here?
				throw new ApplicationException("Couldn't figure out what kind of login we're doing here!");

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Message", ex.Message);
				return View("LogOn");
			}
		}

		/// <summary>
		/// OK, Facebook doesn't wanna PLAY BALL like the rest of the providers,
		/// so we need to parse the "state" from them specialy.
		/// </summary>
		/// <param name="state">The "state" that we sent to Facebook</param>
		/// <returns></returns>
		public ActionResult ReceiveFacebookResponse(string state, string code)
		{
			System.Collections.Specialized.NameValueCollection nvc =
				System.Web.HttpUtility.ParseQueryString(state);

			return ReceiveResponse(nvc["returnUrl"], Convert.ToInt32(nvc["externalLoginProviderId"]));

		}

		/// <summary>
		/// Process the user's return from the external authenticator and redirec tthem appropriately
		/// </summary>
		/// <param name="returnUrl">The URL to which we should return the user at the end</param>
		/// <param name="externalLoginProviderId">The ID of the external provider (Facebook, Twitter, etc.)</param>
		/// <returns></returns>
		public ActionResult ReceiveResponse(string returnUrl, int externalLoginProviderId)
		{
			try
			{
				OAuthToken oauthToken = null;
				User user = null;


				switch(externalLoginProviderId)
				{
					case ExternalLoginProvider.GenericOpenID:
					
						user = LogUserIn(_captService.GetOpenIdIdentifier(System.Web.HttpContext.Current.Request),
							externalLoginProviderId);
						break;

					case ExternalLoginProvider.Facebook:
						
						LogUserIn(
							_captService.GetFacebookId(
								System.Web.HttpContext.Current.Request,
								Url.Action("ReceiveFacebookResponse", "Account", null, Request.Url.Scheme)),
							ExternalLoginProvider.Facebook);

						break;

					case ExternalLoginProvider.Twitter:

						string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
						string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

						user = LogUserIn(
							_captService.GetTwitterId(
								System.Web.HttpContext.Current.Request,
								consumerKey, consumerSecret, out oauthToken),
							externalLoginProviderId);

						break;

					default:

						// Crud.  Couldn't figure out who's sending us back?
						throw new ApplicationException("Couldn't figure out what kind of login we're doing here!");
				}

				// Did we pick up an OAuth token in the process of logging our pal in?
				if (
					user != null 
					&& oauthToken != null
					&& user.OAuthTokens.Where(
						t => t.ExternalLoginProviderId == externalLoginProviderId
					).Count() == 0
					)
				{
					_captService.SaveOAuthToken(user, oauthToken.Expires ?? DateTime.MaxValue,
						oauthToken.ExternalLoginProviderId, oauthToken.Token, oauthToken.Secret);
				}

				// So now then, where was it the user wanted to go?
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
				{
					return Redirect(returnUrl);
				}
				else
				{
					return RedirectToAction("Index", "PictureCaptions");
				}

			}
			catch (Exception e)
			{
				ModelState.AddModelError("Message", e.Message);
				return View("LogOn");
			}


		}

		/// <summary>
		/// Get a user by his External ID. If he's new, create and save him.  Put him in the session.
		/// TODO: Move this to the service layer, as soon as I create it.
		/// </summary>
		/// <param name="externalId">The ID used by the external provider</param>
		/// <param name="externalLoginTypeId">The type of external login (OpenID, Facebook, Twitter, etc.)</param>
		/// <returns></returns>
		private User LogUserIn(string externalId, int externalLoginTypeId)
		{
			User user = _captService.GetOrCreateUser(externalId, externalLoginTypeId);

			Session["User"] = user;
			FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
			return user;
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
	}
}
