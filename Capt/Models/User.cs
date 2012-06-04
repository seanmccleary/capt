﻿/*
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
using System.Web;

namespace Capt.Models
{
	public partial class User
	{

		/// <summary>
		/// The captions score (upvotes - downvotes on this users captions).
		/// </summary>
		public int Score
		{
			get
			{
				return this.getScoreForDateRange();
			}
		}

		public int getScoreForDateRange(DateTime? start = null, DateTime? end = null)
		{
			int score = 0;
			List<Vote> votesOnUsersCaptions = new List<Vote>();

			List<Caption> timelyCaptions;
			if (start != null && end != null)
			{
				timelyCaptions = Captions.Where(c => c.Event.Datetime >= start && c.Event.Datetime <= end).ToList();
			}
			else
			{
				timelyCaptions = Captions.ToList();
			}
			
			foreach (var usersCaption in timelyCaptions)
			{
				score += usersCaption.Votes.Sum(v => v.Weight);
			}

			return score;
		}

	}
}