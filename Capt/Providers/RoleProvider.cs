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
using Capt.Models.Repositories;

namespace Capt.Providers
{
	/// <summary>
	/// Custom role provider.  
	/// </summary>
	public class RoleProvider : System.Web.Security.RoleProvider
	{
		private IUserRepository _userRepo;

		/// <summary>
		/// Create an instance of this role provider with the user repository OF YOUR DREAMS.
		/// </summary>
		/// <param name="userRepo"></param>
		public RoleProvider(IUserRepository userRepo)
		{
			this._userRepo = userRepo;
		}

		/// <summary>
		/// Create an instance of this role provider with the default repository.
		/// </summary>
		public RoleProvider() : this(new Capt.Models.Repositories.LinqToMySql.UserRepository())
		{
		}

		public override string ApplicationName
		{
			get
			{
				return "Capt";
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// There is only one role: Administrator.
		/// </summary>
		/// <returns></returns>
		public override string[] GetAllRoles()
		{
			return new string[] { "Administrator" };
		}

		/// <summary>
		/// Administrator's the only role.
		/// </summary>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public override bool RoleExists(string roleName)
		{
			return roleName == "Administrator";
		}

		public override string[] GetRolesForUser(string username)
		{

			int userId = Convert.ToInt32(username);

			User user = System.Web.HttpContext.Current.Session["User"] as User;
			if( user != null && user.Id == userId && user.IsAdmin )
			{
				return new String[] { "Administrator" };
			}
			else if( user == null || user.Id != userId )
			{
				user = _userRepo.GetById(Convert.ToInt32(username));

				if (user != null && user.IsAdmin)
				{
					return new string[] { "Administrator" };
				}
			}

			return new string[] { };

		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return GetRolesForUser(username).Contains(roleName);
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException("Please just edit the user's IsAdmin property!");
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}


	}
}