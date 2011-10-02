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
 * Foobar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capt.Models;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write User objects to/from the MySQL database.
	/// </summary>
	public class UserRepository : Repository, IUserRepository
	{

		public IEnumerable<User> GetAll()
		{
			return db.Users;
		}

		public User GetByName(string name)
		{
			return (from u in db.Users
					where u.Name == name.Trim()
					select u).SingleOrDefault();
		}

		public User GetByExternalLoginId(string externalLoginId)
		{

			var openIDAuth = (from p in db.ExternalLogins
							  where p.Identifier == externalLoginId
							  select p).SingleOrDefault();

			if (openIDAuth != null)
			{
				return openIDAuth.User;
			}
			else
			{
				return null;
			}

		}

		public User GetById(int userId)
		{
			var user = (from u in db.Users
					where u.Id == userId
					select u).SingleOrDefault();
			return user;
		}

		public void Save(User user)
		{
			if (user.Id == 0)
			{
				try
				{
					// It's unfortunate that I have to create my own transaction here but
					// linq isn't smart
					// enough to figure out the correct order of insert statements.
					db.Connection.Open();
					db.Transaction = db.Connection.BeginTransaction();
					var externalLogin = user.ExternalLogins.Single();
					user.ExternalLogins.Clear();
					externalLogin.User = null;


					db.Events.InsertOnSubmit(user.Event);
					db.SubmitChanges();

					user.ExternalLogins.Add(externalLogin);
					db.Events.InsertOnSubmit(externalLogin.Event);
					db.SubmitChanges();
					db.Transaction.Commit();
					db.Connection.Close();
				}
				catch (Exception)
				{
					// crud.  (pun intended.)
					db.Transaction.Rollback();
					throw;
				}
				
			}
			else
			{
				// Nuts.  He's not new.  Doing a lousy linq2sql update.
				User oldUser = GetById(user.Id);

				oldUser.AdminNote = user.AdminNote;
				oldUser.EmailAddress = user.EmailAddress;
				oldUser.IsAdmin = user.IsAdmin;
				oldUser.IsEmailAddressVerified = user.IsEmailAddressVerified;
				oldUser.IsLocked = user.IsLocked;
				oldUser.Name = user.Name;

				db.SubmitChanges();
			}

			
		}

		public void Delete(User user)
		{
			HashSet<Vote> votesToAxe = new HashSet<Vote>();
			HashSet<Picture> picturesToAxe = new HashSet<Picture>();
			HashSet<Caption> captionsToAxe = new HashSet<Caption>();
			
			// OK there's potentially a lot of things ensnared with the user object here
			// that we also have to delete.

			// First the votes he's cast 
			user.Votes.ToList().ForEach(v => votesToAxe.Add(v));

			// Then his pictures, any votes cast for his pictures, any captions added to his pictures and any
			// votes cast for those captions
			foreach (var picture in user.Pictures)
			{
				picturesToAxe.Add(picture);
				picture.Votes.ForEach(v => votesToAxe.Add(v));
				foreach(var caption in picture.Captions)
				{
					captionsToAxe.Add(caption);
					caption.Votes.ForEach(v => votesToAxe.Add(v));
				}
			}

			// Then his captions, and all votes cast for those captions
			foreach (var caption in user.Captions)
			{
				captionsToAxe.Add(caption);
				caption.Votes.ForEach(v => votesToAxe.Add(v));
			}

			// Now delete them votes and their hangers-on
			foreach (var vote in votesToAxe)
			{
				db.PictureVotes.DeleteAllOnSubmit(vote.PictureVotes);
				db.CaptionVotes.DeleteAllOnSubmit(vote.CaptionVotes);
				db.Votes.DeleteOnSubmit(vote);
				db.Events.DeleteOnSubmit(vote.Event);
			}

			// Now delete captions and their hangers-on
			foreach (var caption in captionsToAxe)
			{
				db.Captions.DeleteOnSubmit(caption);
				db.Events.DeleteOnSubmit(caption.Event);
			}

			// Now delete pictures and their hangers-on
			foreach (var picture in picturesToAxe)
			{
				db.Pictures.DeleteOnSubmit(picture);
				db.Events.DeleteOnSubmit(picture.Event);
			}

			// Then his external logins & their create events
			foreach (var externalLogin in user.ExternalLogins)
			{
				db.ExternalLogins.DeleteOnSubmit(externalLogin);
				db.Events.DeleteOnSubmit(externalLogin.Event);
			}

			// And his OAuth tokens and their create events
			foreach (var oauthToken in user.OAuthTokens)
			{
				db.OAuthTokens.DeleteOnSubmit(oauthToken);
				db.Events.DeleteOnSubmit(oauthToken.Event);
			}
			
			// Finally, the user & his create event
			db.Users.DeleteOnSubmit(user);
			db.Events.DeleteOnSubmit(user.Event);

			db.SubmitChanges();

		}

	}
}