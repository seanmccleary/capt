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
using System.Web.Routing;
using System.Web.Security;
using Capt.Models;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using Facebook;
using Twitterizer;
using System.Text.RegularExpressions;

namespace Capt.Controllers
{
	/// <summary>
	/// Controller for login-related actions
	/// </summary>
	public class AccountController : ApplicationController
	{
		/// <summary>
		/// User repository
		/// </summary>
		private IUserRepository _userRepo;

		/// <summary>
		/// OAuthToken repository
		/// </summary>
		private IOAuthTokenRepository _oauthTokenRepo;

		/// <summary>
		/// This is the URL that users will be redirected to after Facebook is done with 'em.
		/// This is a property because there's two steps in the process where we have to send this
		/// to Facebook.
		/// </summary>
		private string FacebookRedirectUrl
		{
			get
			{
				return
					Request.Url.Scheme + "://"
					+ Request.Url.Host
					+ (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)
					+ Url.Action("ReceiveFBResponse");
			}
		}

		/// <summary>
		/// The zero-argument constructor will instantiate the repository classes that'll be used.
		/// </summary>
		public AccountController()
			: this(
				new Capt.Models.LinqToMySql.UserRepository(),
				new Capt.Models.LinqToMySql.OAuthTokenRepository()
				)
		{
		}

		/// <summary>
		/// Constructor that lets you specify your own repository classes.
		/// </summary>
		/// <param name="userRepo">The user repository you'd like to use</param>
		/// <param name="oauthTokenRepo">The OAuthToken repository you'd like to use</param>
		public AccountController(IUserRepository userRepo, IOAuthTokenRepository oauthTokenRepo)
		{
			this._userRepo = userRepo;
			this._oauthTokenRepo = oauthTokenRepo;
		}

		/// <summary>
		/// The login form submits to this action.
		/// </summary>
		/// <param name="returnUrl">The URL the user should be sent back to once he's logged in</param>
		/// <returns></returns>
		[ValidateInput(false)]
		public ActionResult Authenticate(string returnUrl)
		{

			// Are we doing OpenID here?
			if (Request.Form["provider_type"] == "openid")
			{
				return SendOpenIDRequest(returnUrl);
			}

			// How's about Facebook?
			else if (Request.Form["provider_type"] == "fb")
			{
				return SendFBRequest(returnUrl);
			}

			// Twitter, perhaps?
			else if (Request.Form["provider_type"] == "twitter")
			{
				return SendTwitterRequest(returnUrl);
			}


			return new EmptyResult();
		}

		/// <summary>
		/// Send the user off to his chosen Open ID provider so's he can get authenticated
		/// </summary>
		/// <param name="returnUrl">The URL back to which the user should be redirected once he's logged in</param>
		/// <returns></returns>
		private ActionResult SendOpenIDRequest(string returnUrl)
		{

			OpenIdRelyingParty openid = new OpenIdRelyingParty();

			// Stage 2: user submitting Identifier  
			Identifier id;
			if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
			{
				try
				{
					// This is the URL that the Open ID server should send the user back to
					// NOT the one that WE will eventually redirect the user back to.
					Uri sendBackUri = new Uri(
						Request.Url.Scheme + "://"
						+ Request.Url.Host
						+ (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)
						+ Url.Action("ReceiveOpenIDResponse", new { returnUrl = returnUrl }));

					string realmUrl = Request.Url.Scheme + "://"
						+ Request.Url.Host
						+ (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

					Regex regex = new Regex("(https?)://.*\\.?(captrato.com)");
					realmUrl = regex.Replace(realmUrl, "$1://*.$2");

					Realm realm = new Realm(realmUrl);
					var request = openid.CreateRequest(Request.Form["openid_identifier"], realm, sendBackUri);

					//Ask user for their email address  
					/* Nahh...
					ClaimsRequest fields = new ClaimsRequest();
					fields.Email = DemandLevel.Request;
					request.AddExtension(fields);
					*/
					return request.RedirectingResponse.AsActionResult();
				}
				catch (ProtocolException ex)
				{
					ModelState.AddModelError("Message", ex.Message);
					return View("LogOn");
				}
			}
			ModelState.AddModelError("Message", "Invalid identifier");
			return View("LogOn");
		}

		/// <summary>
		/// Handle a user returning from his Open ID provider.
		/// </summary>
		/// <param name="returnUrl">The URL we need to return the user to when all is said and done</param>
		/// <returns></returns>
		public ActionResult ReceiveOpenIDResponse(string returnUrl)
		{

			OpenIdRelyingParty openid = new OpenIdRelyingParty();

			IAuthenticationResponse response;

			if ((response = openid.GetResponse()) == null)
			{
				// TODO: Handle this better.
				return RedirectToAction("LogOn");
			}

			switch (response.Status)
			{

				case AuthenticationStatus.Authenticated:
					LogUserIn(response.ClaimedIdentifier, ExternalLoginProvider.GenericOpenID);

					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}

					return RedirectToAction("Index", "PictureCaptions");

				case AuthenticationStatus.Canceled:
					ModelState.AddModelError("Message", "Canceled at provider");
					return View("LogOn");

				case AuthenticationStatus.Failed:
					ModelState.AddModelError("Message", response.Exception);
					return View("LogOn");

				default:
					ModelState.AddModelError("Message", "There was a problem logging in.");
					return View("LogOn");
					
			}
		}

		/// <summary>
		/// Send the user off to Facebook to log in there.
		/// </summary>
		/// <param name="returnUrl">The URL the user should eventually be sent back to</param>
		/// <returns></returns>
		private ActionResult SendFBRequest(string returnUrl = "/")
		{
			FacebookOAuthClient FBClient = new FacebookOAuthClient(FacebookApplication.Current);

			FBClient.RedirectUri = new Uri(FacebookRedirectUrl);
			var loginUri = FBClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl } });

			return Redirect(loginUri.AbsoluteUri);
		}

		/// <summary>
		/// Receive the user after Facebook's send him back
		/// </summary>
		/// <param name="code">The code given back to us from Facebook</param>
		/// <param name="returnUrl">The URL we should return the user to when he's logged in</param>
		/// <returns></returns>
		public ActionResult ReceiveFBResponse(string code, [Bind(Prefix="state")] string returnUrl)
		{

			FacebookOAuthResult oauthResult;
			if (!FacebookOAuthResult.TryParse(Request.Url, out oauthResult))
			{
				ModelState.AddModelError("Message", "There was a problem logging in through facebook!");
				return RedirectToAction("LogOn");
			}

			if (!oauthResult.IsSuccess)
			{
				ModelState.AddModelError("Message", oauthResult.ErrorDescription);
				return View("LogOn");
			}

			var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
			oAuthClient.RedirectUri = new Uri(FacebookRedirectUrl);
			dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(code);
			string accessToken = tokenResult.access_token;

			DateTime expiresOn = DateTime.MaxValue;

			if (tokenResult.ContainsKey("expires"))
			{
				DateTimeConvertor.FromUnixTime(tokenResult.expires);
			}

			FacebookClient fbClient = new FacebookClient(accessToken);
			dynamic me = fbClient.Get("me?fields=id");

			LogUserIn(me.id, ExternalLoginProvider.Facebook);

			// prevent open redirection attack by checking if the url is local.
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		/// <summary>
		/// Send the user off to Twitter to get hisself authenticated.
		/// </summary>
		/// <param name="returnUrl">The URL we should eventually redirect the user back to</param>
		/// <returns></returns>
		public ActionResult SendTwitterRequest(string returnUrl = "/")
		{
			string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
			string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

			returnUrl = Request.Url.Scheme + "://"
			+ Request.Url.Host
			+ (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)
			+ Url.Action("ReceiveTwitterResponse", new { returnUrl = returnUrl });

			var requestToken = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, returnUrl);

			return Redirect("http://twitter.com/oauth/authenticate?oauth_token=" + requestToken.Token);

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

			if (!String.IsNullOrWhiteSpace(Request["denied"]))
			{
				ModelState.AddModelError("Message", "Couldn't log you in via Twitter. (Did you deny access?)");
				return View("LogOn");
			}

			string consumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerKey"];
			string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterConsumerSecret"];

			OAuthTokenResponse tokens = OAuthUtility.GetAccessToken(consumerKey, consumerSecret, oauth_token, oauth_verifier);

			string userId = Convert.ToString(tokens.UserId);


			var user = LogUserIn(userId, ExternalLoginProvider.Twitter);

			// Might as well save their Twitter token now, case'n they wanna share something later
			var valid_tokens = from t in user.OAuthTokens
							   where t.ExternalLoginProviderId == ExternalLoginProvider.Twitter
							   && t.Expires > DateTime.UtcNow
							   select t;

			if (valid_tokens.Count() == 0)
			{
				var twitter_token = new OAuthToken()
				{
					Expires = DateTime.MaxValue,
					ExternalLoginProviderId = ExternalLoginProvider.Twitter,
					Secret = tokens.TokenSecret,
					Token = tokens.Token,
					User = user,
					Event = new Event(EventType.ExternalLoginCreated)
				};

				_oauthTokenRepo.Save(twitter_token);
			}

			// prevent open redirection attack by checking if the url is local.
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return Redirect("/");
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

			User user = _userRepo.GetByExternalLoginId(externalId);

			if (user == null)
			{
				// well kiss my grits!  they're new here!
				user = new User
				{
					Guid = Guid.NewGuid(),
					IsAdmin = false,
					IsLocked = false,
					IsEmailAddressVerified = false,
					Event = new Event(EventType.UserCreated)
				};

				var openIDAuth = new ExternalLogin()
				{
					Identifier = externalId,
					User = user,
					ExternalLoginProviderId = externalLoginTypeId,
					Event = new Event(EventType.ExternalLoginCreated)
				};

				user.ExternalLogins.Add(openIDAuth);


			}

			// Update his last login datetime
			user.LastLogin = DateTime.UtcNow;
			_userRepo.Save(user);

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
