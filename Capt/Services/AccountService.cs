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
using Capt.Models;

namespace Capt.Services
{
	public class AccountService : IAccountService
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
		/// The zero-argument constructor will use a default service layer
		/// </summary>
		public AccountService()
			: this(
				new Capt.Models.LinqToMySql.UserRepository(),
				new Capt.Models.LinqToMySql.OAuthTokenRepository()
			)
		{
		}

		/// <summary>
		/// Constructor that lets you specify your own service layer
		/// </summary>
		/// <param name="_captService">The service layer object you'd like to use</param>
		public AccountService(IUserRepository userRepo, IOAuthTokenRepository oauthTokenRepo) 
		{
			_userRepo = userRepo;
			_oauthTokenRepo = oauthTokenRepo;
		}

		/// <see cref="Capt.Services.IAccountService.GetUserById"/>
		public User GetUserById(int userId)
		{
			return _userRepo.GetById(userId);
		}

		/// <see cref="Capt.Services.IAccountService.GetUserByName"/>
		public User GetUserByName(string userName)
		{
			return _userRepo.GetByName(userName);
		}

		/// <see cref="Capt.Services.IAccountService.GetRankedUsers"/>
		public List<User> GetRankedUsers()
		{

			// TODO: This is just... Jesus.  I'm sorting by something whih
			// isn't actually in the database.  That's the problem. It requires
			// selecting ALL users first.  This oughts be fixed up.
			return (from u in _userRepo.GetAll()
						where !u.IsLocked
						&& u.Name != null
						select u).ToList().OrderByDescending(u => u.Score).ToList();

		}

		/// <see cref="Capt.Services.IAccountService.GetOrCreateUser"/>
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

		/// <see cref="Capt.Services.IAccountService.SaveUser" />
		public void SaveUser(User user)
		{
			_userRepo.Save(user);
		}

		/// <see cref="Capt.Services.IAccountService.GetAllUsers" />
		public List<User> GetAllUsers()
		{
			return _userRepo.GetAll().ToList();
		}

		/// <see cref="Capt.Services.IAccountService.DeleteUser" />
		public void DeleteUser(int userId)
		{
			DeleteUser(GetUserById(userId));
		}
		
		/// <see cref="Capt.Services.IAccountService.DeleteUser" />
		public void DeleteUser(User user)
		{
			_userRepo.Delete(user);
		}

		/// <see cref="Capt.Services.IAccountService.SaveOAuthToken" />
		public void SaveOAuthToken(User user, DateTime expiration, int externalLoginProviderId, string token, string secret)
		{
			var new_token = new Capt.Models.OAuthToken()
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