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

namespace Capt.Controllers
{
	/// <summary>
	/// Mostly just some scaffolded actions to be used with ugly, scaffolded views to do some basic admin stuff.  
	/// Nothing user-facing here.
	/// </summary>
	[Authorize(Roles = "Administrator")]
	public class PictureController : ApplicationController
	{

		private IPictureRepository _pictureRepo;

		private ILicenseRepository _licenseRepo;

		/// <summary>
		/// Create an instance of this controller with the specified repositories.
		/// </summary>
		/// <param name="pictureRepo"></param>
		/// <param name="licenseRepo"></param>
		public PictureController(IPictureRepository pictureRepo, ILicenseRepository licenseRepo)
		{
			_pictureRepo = pictureRepo;
			_licenseRepo = licenseRepo;

			var LicenseOptions = new List<SelectListItem>();
			var licenses = _licenseRepo.GetAll();
			foreach(var license in licenses)
			{
				LicenseOptions.Add(
					new SelectListItem()
					{
						Value = license.Id.ToString(),
						Text = license.Description
					}
				);
			}

			ViewBag.LicenseOptions = LicenseOptions;

		}

		/// <summary>
		/// Create an instance of this controller with the default repositories.
		/// </summary>
		public PictureController()
			: this(new Capt.Models.LinqToMySql.PictureRepository(), new Capt.Models.LinqToMySql.LicenseRepository())
		{
		}

		//
		// GET: /Picture/Details/5
		public ActionResult Details(int id)
		{
			Picture pic = _pictureRepo.GetById(id);

			ViewBag.Picture = pic;


			return View(pic);

		}

		//
		// GET: /Picture/Create
		public ActionResult Create()
		{
			Picture pic = new Picture()
			{
				IsNSFW = false,
				IsPrivate = false,
				IsVisible = true,
				Activates = DateTime.UtcNow
			};

			return View(pic);
		}

		//
		// POST: /Picture/Create

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(Picture newPicture)
		{
			try
			{

				if (!ModelState.IsValid)
				{
					return View();
				}

				newPicture.Guid = Guid.NewGuid();
				newPicture.User = Session["User"] as User;
				newPicture.Event = new Event(EventType.PictureCreated);

				_pictureRepo.Save(newPicture);

				return RedirectToAction("Index", "PictureCaptions");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Picture/Edit/5
		public ActionResult Edit(int id)
		{
			return View(_pictureRepo.GetById(id));
		}

		//
		// POST: /Picture/Edit/5

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(int id, Picture editedPicture)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View();
				}

				_pictureRepo.Save(editedPicture);

				return View();
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /Picture/Delete/5
		public ActionResult Delete(int id)
		{
			return View(_pictureRepo.GetById(id));
		}

		//
		// POST: /Picture/Delete/5

		[HttpPost]
		public ActionResult Delete(int id, Picture picture)
		{
			picture = _pictureRepo.GetById(picture.Id);
			_pictureRepo.Delete(picture);
			return RedirectToAction("Index", "PictureCaptions");
		}

	}
}
