using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface for reading users from and saving users to the data store.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Get all the users from the database.
		/// </summary>
		/// <returns></returns>
		IEnumerable<User> GetAll();

		/// <summary>
		/// Get a user by his nickname.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		User GetByName(string name);

		/// <summary>
		/// Get a user by his external login ID
		/// </summary>
		/// <param name="externalLoginId">This might be, for example, an OpenID identifier</param>
		/// <returns>The requested User, or null if not found.</returns>
		User GetByExternalLoginId(string externalLoginId);

		/// <summary>
		/// Get a user by his database Id.
		/// </summary>
		/// <param name="userID">The database Id</param>
		/// <returns>The requested user, or null if not found</returns>
		User GetById(int userId);

		/// <summary>
		/// Save a user to the database, doing an insert if he's new or update if not.
		/// </summary>
		/// <param name="user">The user to save.</param>
		void Save(User user);

		/// <summary>
		/// Delete a user from the data store.
		/// </summary>
		/// <param name="user">The user to delete</param>
		void Delete(User user);
	}
}
