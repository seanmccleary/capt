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
	/// Basic scaffolded controller for use with ugly scaffolded views for some admin tasks.  Nothing user-facing here.
	/// </summary>
	[Authorize(Roles = "Administrator")]
	[GlobalViewData]
	public class UserController : Controller
	{
		/// <summary>
		/// Our account service for this controller.
		/// </summary>
		private IAccountService _accountService;

		/// <summary>
		/// Create an instance of this controller with the specified service layer.
		/// </summary>
		/// <param name="accountService">The account service layer to use</param>
		public UserController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		/// <summary>
		/// Create an intsance of this controlelr with the user repository of your choice.
		/// </summary>
		public UserController() : this(new AccountService())
		{
		}

		//
		// GET: /User/
		
		public ActionResult Index()
		{
			return View(_accountService.GetAllUsers());
		}

		//
		// GET: /User/Details/5
		
		public ActionResult Details(int id)
		{
			return View(_accountService.GetUserById(id));
		}


		//
		// GET: /User/Edit/5
		
		public ActionResult Edit(int id)
		{
			return View(_accountService.GetUserById(id));
		}

		//
		// POST: /User/Edit/5
		
		[HttpPost]
		public ActionResult Edit(int id, User user)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View();
				}
				_accountService.SaveUser(user);

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		//
		// GET: /User/Delete/5
		
		public ActionResult Delete(int id)
		{
			return View(_accountService.GetUserById(id));
		}

		//
		// POST: /User/Delete/5
		
		[HttpPost]
		public ActionResult Delete(int id, User user)
		{
			try
			{
				_accountService.DeleteUser(id);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
