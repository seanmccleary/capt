using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capt.Models
{
	/// <summary>
	/// A repository interface to get Picture objects into/out of the data store.
	/// </summary>
	public interface IPictureRepository
	{
		/// <summary>
		/// Get all Picture objects.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Picture> GetAll();

		/// <summary>
		/// Get the Picture objects sorted by rank, and filtered appropriately
		/// </summary>
		/// <param name="start">Which result number do you want to start on?</param>
		/// <param name="take">How many results do you want?</param>
		/// <param name="isForAdmin">Is this going to be shown to an admin, in privilege mode?</param>
		/// <returns></returns>
		IEnumerable<Picture> GetRanked(int start, int take, bool isForAdmin);

		/// <summary>
		/// Get a Picture object by its ID
		/// </summary>
		/// <param name="pictureId">The ID of the Picture object you want</param>
		/// <returns></returns>
		Picture GetById(int pictureId);

		/// <summary>
		/// Get the next picture after another given picture
		/// </summary>
		/// <param name="picture">The picture for which you want the next picture</param>
		/// <returns></returns>
		Picture GetNext(Picture picture);

		/// <summary>
		/// Get the previous picture before another given picture.
		/// </summary>
		/// <param name="picture">The picture for which you want the previous picture</param>
		/// <returns></returns>
		Picture GetPrevious(Picture picture);

		/// <summary>
		/// Get a picture by its attribution URL (used to see if a picture from Flickr's already been used on the site)
		/// </summary>
		/// <param name="url">The URL for the picture</param>
		/// <returns></returns>
		Picture GetByUrl(string url);

		/// <summary>
		/// Save (insert or update) a picture to the data store
		/// </summary>
		/// <param name="picture">The picture you want to save</param>
		void Save(Picture picture);

		/// <summary>
		/// Delete a picture from the data store.
		/// </summary>
		/// <param name="picture">The picture you want to delete</param>
		void Delete(Picture picture);
	}
}
