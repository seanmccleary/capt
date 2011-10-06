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
				// Are we doing OpenID here?
				if (Request.Form["provider_type"] == "openid")
				{
					return Redirect(_captService.GetOpenIdRedirectUrl(Request.Form["openid_identifier"], Request.Url,
						Url.Action("ReceiveOpenIDResponse", new { returnUrl = returnUrl })));
				}

				// How's about Facebook?
				else if (Request.Form["provider_type"] == "fb")
				{
					return Redirect(_captService.GetFacebookRedirectUrl(
						Url.Action("ReceiveFBResponse", "Account", null, Request.Url.Scheme),
						returnUrl)
					);
				}

				// Twitter, perhaps?
				else if (Request.Form["provider_type"] == "twitter")
				{
					string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
					string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];


					return Redirect(_captService.GetTwitterRedirectUrl(
						Url.Action("ReceiveTwitterResponse", "Account", null, Request.Url.Scheme),
						returnUrl,
						consumerKey,
						consumerSecret)
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
		/// Handle a user returning from his Open ID provider.
		/// </summary>
		/// <param name="returnUrl">The URL we need to return the user to when all is said and done</param>
		/// <returns></returns>
		public ActionResult ReceiveOpenIDResponse(string returnUrl)
		{
			try
			{
				LogUserIn(_captService.GetOpenIdIdentifier(System.Web.HttpContext.Current.Request), 
					ExternalLoginProvider.GenericOpenID);

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
		/// Receive the user after Facebook's sent him back
		/// </summary>
		/// <param name="code">The code given back to us from Facebook</param>
		/// <param name="returnUrl">The URL we should return the user to when he's logged in</param>
		/// <returns></returns>
		public ActionResult ReceiveFBResponse(string code, [Bind(Prefix="state")] string returnUrl)
		{

			try
			{
				LogUserIn(_captService.GetFacebookId(System.Web.HttpContext.Current.Request),
					ExternalLoginProvider.Facebook);

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
		/// Receive the user coming back from Twitter after having been authenticated
		/// </summary>
		/// <param name="oauth_token">OAuth token from Twitter</param>
		/// <param name="oauth_verifier">OAuth verifier from Twitter</param>
		/// <param name="returnUrl">The URL we should eventually redirect the user back to</param>
		/// <returns></returns>
		public ActionResult ReceiveTwitterResponse(string oauth_token, string oauth_verifier, string returnUrl)
		{
			string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
			string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

			try
			{
				OAuthToken oauthToken = null;
				User user = LogUserIn(
					_captService.GetTwitterId(
						System.Web.HttpContext.Current.Request,
						consumerKey, consumerSecret, out oauthToken),
					ExternalLoginProvider.Facebook);

				if (
					oauthToken != null
					&& user.OAuthTokens.Where(t => t.ExternalLoginProviderId == ExternalLoginProvider.Twitter).Count() == 0
					)
				{
					_captService.SaveOAuthToken(user, oauthToken.Expires ?? DateTime.MaxValue, 
						oauthToken.ExternalLoginProviderId,	oauthToken.Token, oauthToken.Secret);
				}

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
