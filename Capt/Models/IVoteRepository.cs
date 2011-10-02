using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface for reading Vote objects from and writing them to the data store.
	/// </summary>
	public interface IVoteRepository
	{
		/// <summary>
		/// Get all Vote objects for a given Caption
		/// </summary>
		/// <param name="captionId">ID of the caption whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByCaptionId(int captionId);

		/// <summary>
		/// Get all Vote objects for a given Picture
		/// </summary>
		/// <param name="pictureId">ID of the Picture whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByPictureId(int pictureId);

		/// <summary>
		/// Get all Vote objects created by a given User
		/// </summary>
		/// <param name="userId">ID of the user whose votes you want</param>
		/// <returns></returns>
		IEnumerable<Vote> GetByUserId(int userId);

		/// <summary>
		/// Save (insert or update) a Vote to the data store
		/// </summary>
		/// <param name="vote">The vote you'd like to save</param>
		void Save(Vote vote);

		/// <summary>
		/// Delete a given vote from the data store
		/// </summary>
		/// <param name="vote">The Vote you'd like to delete</param>
		void Delete(Vote vote);
	}
}
