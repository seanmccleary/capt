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
using System.Text.RegularExpressions;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Facebook;
using System.Collections.Generic;
using Twitterizer;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// TODO: Make some better exceptions to throw than ApplicationException
	/// </summary>
	public class ExternalLoginService : IExternalLoginService
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
		/// Constructor which lets you specify which repositories you'd like to use.
		/// </summary>
		/// <param name="userRepo"></param>
		/// <param name="oauthTokenRepo"></param>
		public ExternalLoginService(
			IUserRepository userRepo,
			IOAuthTokenRepository oauthTokenRepo
			)
		{
			_userRepo = userRepo;
			_oauthTokenRepo = oauthTokenRepo;
		}

		/// <summary>
		/// Zero-argument constructor which uses the default repositories.
		/// </summary>
		public ExternalLoginService()
			: this(
			new Models.LinqToMySql.UserRepository(),
			new Models.LinqToMySql.OAuthTokenRepository()
			)
		{
		}

		/// <see cref="Capt.Models.IExternalLoginService.LogUserIn" />
		public User GetOrCreateUser(string externalId, int externalLoginTypeId)
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

			return user;
		}

		/// <see cref="Capt.Models.IExternalLoginService.GetOpenIdRedirectUrl" />
		public string GetOpenIdRedirectUrl(string identifier, System.Uri request, string returnUrl)
		{

			OpenIdRelyingParty openid = new OpenIdRelyingParty();
			Identifier id;
			Identifier.TryParse(identifier, out id);

			// This is the URL that the Open ID server should send the user back to
			// NOT the one that WE will eventually redirect the user back to.
			Uri sendBackUri = new Uri(
				request.Scheme + "://"
				+ request.Host
				+ (request.IsDefaultPort ? "" : ":" + request.Port)
				+ returnUrl);

			string realmUrl = request.Scheme + "://"
				+ request.Host
				+ (request.IsDefaultPort ? "" : ":" + request.Port);

			// TODO: Don't hard-code this to captrato.com
			Regex regex = new Regex("(https?)://.*\\.?(captrato.com)");
			realmUrl = regex.Replace(realmUrl, "$1://*.$2");

			Realm realm = new Realm(realmUrl);
			var openidRequest = openid.CreateRequest(identifier, realm, sendBackUri);

			//Ask user for their email address  
			/* Nahh...
			ClaimsRequest fields = new ClaimsRequest();
			fields.Email = DemandLevel.Request;
			request.AddExtension(fields);
			*/
			//return openidRequest.RedirectingResponse.AsActionResult();
			return openidRequest.RedirectingResponse.Headers["Location"];
		}

		/// <see cref="Capt.Models.IExternalLoginService.GetOpenIdIdentifier"/>
		public string GetOpenIdIdentifier(System.Web.HttpRequest request)
		{
			OpenIdRelyingParty openid = new OpenIdRelyingParty();

			IAuthenticationResponse response;

			if ((response = openid.GetResponse(new HttpRequestInfo(request))) == null)
			{
				throw new ApplicationException("No OpenID response");
			}

			switch (response.Status)
			{
				case AuthenticationStatus.Authenticated:
					return response.ClaimedIdentifier;

				case AuthenticationStatus.Canceled:
					throw new ApplicationException("Canceled at provider");

				case AuthenticationStatus.Failed:
					throw response.Exception;

				default:
					throw new ApplicationException("There was a problem logging in.");
			}

		}

		/// <see cref="Capt.Models.IExternalLoginService.GetFacebookRedirectUrl"/>
		public string GetFacebookRedirectUrl(string receiveUrl, string returnUrl)
		{
			FacebookOAuthClient FBClient = new FacebookOAuthClient(FacebookApplication.Current);

			FBClient.RedirectUri = new Uri(receiveUrl);
			var loginUri = FBClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl } });

			return loginUri.AbsoluteUri;

		}

		/// <see cref="Capt.Models.IExternalLoginService.GetFacebookId"/>
		public string GetFacebookId(System.Web.HttpRequest request)
		{
			FacebookOAuthResult oauthResult;
			if (!FacebookOAuthResult.TryParse(request.Url, out oauthResult))
			{
				throw new ApplicationException("There was a problem logging in through Facebook!");
			}

			if (!oauthResult.IsSuccess)
			{
				throw new ApplicationException(oauthResult.ErrorDescription);
			}

			var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
			
			oAuthClient.RedirectUri = new Uri(
					request.Url.Scheme + "://"
					+ request.Url.Host
					+ (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port)
					+ request.Url.AbsolutePath
			);
			dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(request["code"]);
			string accessToken = tokenResult.access_token;

			DateTime expiresOn = DateTime.MaxValue;

			if (tokenResult.ContainsKey("expires"))
			{
				DateTimeConvertor.FromUnixTime(tokenResult.expires);
			}

			FacebookClient fbClient = new FacebookClient(accessToken);
			dynamic me = fbClient.Get("me?fields=id");

			return me.id;
		}

		/// <see cref="Capt.Models.IExternalLoginService.GetTwitterRedirectUrl"/>
		public string GetTwitterRedirectUrl(string receiveUrl, string returnUrl, 
			string consumerKey, string consumerSecret)
		{

			var requestToken = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, receiveUrl + "?returnUrl=" + returnUrl);

			return "http://twitter.com/oauth/authenticate?oauth_token=" + requestToken.Token;
		}

		/// <see cref="Capt.Models.IExternalLoginService.GetTwitterId"/>
		public string GetTwitterId(System.Web.HttpRequest request, string consumerKey, string consumerSecret,
			out OAuthToken oauthToken)
		{

			if (!String.IsNullOrWhiteSpace(request["denied"]))
			{
				throw new ApplicationException("Couldn't log you in via Twitter. (Did you deny access?)");
			}

			OAuthTokenResponse tokens = OAuthUtility.GetAccessToken(consumerKey, consumerSecret, 
				request["oauth_token"], request["oauth_verifier"]);

			string userId = Convert.ToString(tokens.UserId);

			oauthToken = new OAuthToken
			{
				Expires = DateTime.MaxValue,
				ExternalLoginProviderId = ExternalLoginProvider.Twitter,
				Secret = tokens.TokenSecret,
				Token = tokens.Token
			};

			return userId;
		}

		/// <see cref="Capt.Models.IExternalLoginService.SaveOAuthToken"/>
		public void SaveOAuthToken(User user, DateTime expiration, int externalLoginProviderId, string token, string secret)
		{
			var new_token = new OAuthToken()
			{
				Expires = expiration,
				ExternalLoginProviderId = externalLoginProviderId,
				Secret = secret,
				Token = token,
				User = user,
				Event = new Event(EventType.ExternalLoginCreated)
			};

			_oauthTokenRepo.Save(new_token);

		}
	}
}