using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface for getting OAuthToken objects into or out of the data store.
	/// </summary>
	public interface IOAuthTokenRepository
	{
		/// <summary>
		/// Save an OAuthToken to the data store
		/// </summary>
		/// <param name="oauthToken">The OAuthToken you want to save</param>
		void Save(OAuthToken oauthToken);
	}
}
