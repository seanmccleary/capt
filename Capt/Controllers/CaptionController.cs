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
using System.Web;
using System.Web.Mvc;
using Capt.Models;
using Capt.Services;

namespace Capt.Controllers
{

	/// <summary>
	/// A very basic, scaffolded controller for the Caption model.  This is only used for ugly admin stuff, not for
	/// anything user-facing.
	/// </summary>
	[Authorize(Roles = "Administrator")]
	public class CaptionController : Controller
    {

		private IPictureService _pictureService;

		public CaptionController(IPictureService pictureService)
		{
			_pictureService = pictureService; ;
		}

		public CaptionController()
			: this(new PictureService())
		{
		}

        //
        // GET: /Caption/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Caption/Details/5

        public ActionResult Details(int id)
        {
            return View(_pictureService.GetCaptionById(id, ViewBag.IsAdminStuffShown));
        }

        
        //
        // GET: /Caption/Edit/5
 
        public ActionResult Edit(int id)
        {
			return View(_pictureService.GetCaptionById(id, ViewBag.IsAdminStuffShown));
        }

        //
        // POST: /Caption/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Caption caption)
        {
            try
            {
				if (!ModelState.IsValid)
				{
					return View();
				}

				_pictureService.SaveCaption(caption);

				return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Caption/Delete/5
 
        public ActionResult Delete(int id)
        {
			return View(_pictureService.GetCaptionById(id, ViewBag.IsAdminStuffShown));
        }

        //
        // POST: /Caption/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Caption caption)
        {
            try
            {
				caption = _pictureService.GetCaptionById(id, ViewBag.IsAdminStuffShown);
				_pictureService.DeleteCaption(caption);
                return RedirectToAction("Create", "PictureCaptions", new { pictureId = caption.PictureId });
            }
            catch
            {
                return View();
            }
        }
    }
}
