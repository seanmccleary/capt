using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write OAuthToken objects to/from the MySQL database.
	/// </summary>
	public class OAuthTokenRepository : Repository, IOAuthTokenRepository
	{
		public void Save(OAuthToken oauthToken)
		{
			db.Events.InsertOnSubmit(oauthToken.Event);
			db.SubmitChanges();
		}
	}
}