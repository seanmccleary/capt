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
 * capt is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with capt.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capt.Models;

namespace Capt.Services
{
	public interface IPictureService
	{

		Caption GetCaptionById(int captionId, bool isAdmin);

		void SaveCaption(Caption caption);

		void DeleteCaption(int captionId);

		void DeleteCaption(Caption caption);

		List<Caption> GetAllCaptions(bool isAdmin, int? limit);

		List<Comment> GetCommentsForCaption(int captionId, bool isAdmin);

		void SaveCommentForCaption(Comment comment, int captionId);

		Picture GetPictureById(int pictureId, bool isAdmin);

		List<Picture> GetAllPictures(bool isAdmin);

		List<Picture> GetRankedPictures(int count, int start, bool isAdmin);

		Picture GetNextPictureToActivate();

		Picture GetPreviousPicture(Picture picture);

		Picture GetNextPicture(Picture picture);

		void SavePicture(Picture picture);

		void DeletePicture(int pictureId);

		void DeletePicture(Picture picture);

		List<License> GetAllLicenses();

		int AddVoteForPicture(User user, Picture picture, bool isUpvote);

		int AddVoteForCaption(User user, Caption caption, bool isUpvate);

	}
}
