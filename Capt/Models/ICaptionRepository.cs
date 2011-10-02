using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// Repository class to get Caption objects into and out of the data store.
	/// </summary>
	public interface ICaptionRepository
	{
		/// <summary>
		/// Get a Caption object by its ID.
		/// </summary>
		/// <param name="captionId">The ID of the caption you're after.</param>
		/// <returns></returns>
		Caption GetById(int captionId);

		/// <summary>
		/// Save (insert or update) a Caption to the data store.
		/// </summary>
		/// <param name="caption">The caption you're saving</param>
		void Save(Caption caption);

		/// <summary>
		/// Delete a caption from the data store
		/// </summary>
		/// <param name="caption">The caption you want to axe</param>
		void Delete(Caption caption);

		/// <summary>
		/// Get all captions from the data store.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Caption> GetAll();
	}
}
