using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Base class for the DbLinq linq to mysql repositories.
	/// </summary>
	public abstract class Repository
	{
		protected Models.Capt db = new Models.Capt(
			new MySql.Data.MySqlClient.MySqlConnection(
				System.Configuration.ConfigurationManager.ConnectionStrings["capt"].ConnectionString
			)
		);
	}
}