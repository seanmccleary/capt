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
using System.Text;
using Capt.Models;

namespace Capt.Services
{
	public interface IAccountService
	{
		/// <summary>
		/// Get a user from the database based on his external ID (open ID identifier, Twitter ID, etc.), 
		/// creating him first if necessary.
		/// </summary>
		/// <param name="externalId">The user's external ID (openID identifier, Twitter ID, etc.)</param>
		/// <param name="externalLoginTypeId">The login provider ID</param>
		/// <returns></returns>
		User GetOrCreateUser(string externalId, int externalLoginTypeId);

		/// <summary>
		/// Get a user from the DB by his ID
		/// </summary>
		/// <param name="userId">The user's ID (surprise!)</param>
		/// <returns>The user (surprise again!)</returns>
		User GetUserById(int userId);

		/// <summary>
		/// Get a user from the DB by his name
		/// </summary>
		/// <param name="userName">the user's name</param>
		/// <returns>The user</returns>
		User GetUserByName(string userName);

		/// <summary>
		/// Get a list of the users, ranked by score
		/// </summary>
		/// <returns>A list of users, ranked by score</returns>
		List<RankedUser> GetRankedUsers(DateTime? start = null, DateTime? end = null);

		/// <summary>
		/// Get all the users.  (Just wait 'til you get a load of the return value description.)
		/// </summary>
		/// <returns>All the users</returns>
		List<User> GetAllUsers();

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="userId">The numeric ID for the user</param>
		void DeleteUser(int userId);

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="user">The user to delete</param>
		void DeleteUser(User user);

		/// <summary>
		/// Save a user to the data store.
		/// </summary>
		/// <param name="user">The user.</param>
		void SaveUser(User user);

		/// <summary>
		/// Create and save an OAuth token to the database
		/// </summary>
		/// <param name="user">The owner of the oauth token</param>
		/// <param name="expiration">The expiration date of the oauth token</param>
		/// <param name="externalLoginProviderId">The issuer of the OAuth token</param>
		/// <param name="token">The text value of the token</param>
		/// <param name="secret">The token secret</param>
		void SaveOAuthToken(User user, DateTime expiration, int externalLoginProviderId, string token, string secret);


	}
}
