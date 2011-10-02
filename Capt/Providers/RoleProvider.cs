using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capt.Models;

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
		public RoleProvider() : this(new Capt.Models.LinqToMySql.UserRepository())
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