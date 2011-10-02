using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// Repository interface for getting License objects in to or out of the data store.
	/// </summary>
	public interface ILicenseRepository
	{
		/// <summary>
		/// Get all the License objects.
		/// </summary>
		/// <returns></returns>
		IEnumerable<License> GetAll();
	}
}
