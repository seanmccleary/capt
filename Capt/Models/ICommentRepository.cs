using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capt.Models;

namespace Capt.Models
{
	/// <summary>
	/// Repository interface for getting Comments into and out of the data store.
	/// </summary>
	public interface ICommentRepository
	{
		/// <summary>
		/// Get a comment by its ID
		/// </summary>
		/// <param name="captionId">The ID of the comment you're after</param>
		/// <returns></returns>
		IEnumerable<Comment> GetByCaptionId(int captionId);

		/// <summary>
		/// Save (update or insert) a comment into the data store.
		/// </summary>
		/// <param name="comment">The commentr you want to save</param>
		void Save(Comment comment);
	}
}
