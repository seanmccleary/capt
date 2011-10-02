using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capt.Models.LinqToMySql
{
	/// <summary>
	/// Functionality to read/write License objects to/from the MySQL database.
	/// </summary>
	public class LicenseRepository : Repository, ILicenseRepository
	{
		public IEnumerable<License> GetAll()
		{
			return db.Licenses;
		}
	}
}