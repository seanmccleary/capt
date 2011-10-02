using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capt.Models;

namespace Capt.Controllers
{

	/// <summary>
	/// Basic scaffolded controller for use with ugly scaffolded views for some admin tasks.  Nothing user-facing here.
	/// </summary>
	[Authorize(Roles = "Administrator")]
	public class UserController : ApplicationController
	{
		private IUserRepository _userRepo;

		/// <summary>
		/// Create an instance of this controller with the specified user repository.
		/// </summary>
		/// <param name="userRepo"></param>
		public UserController(IUserRepository userRepo)
		{
			_userRepo = userRepo;
		}

		/// <summary>
		/// Create an intsance of this controlelr with the user repository of your choice.
		/// </summary>
		public UserController() : this(new Capt.Models.LinqToMySql.UserRepository())
		{
		}

		//
		// GET: /User/
		
		public ActionResult Index()
		{
			return View(_userRepo.GetAll());
		}

		//
		// GET: /User/Details/5
		
		public ActionResult Details(int id)
		{
			return View(_userRepo.GetById(id));
		}


		//
		// GET: /User/Edit/5
		
		public ActionResult Edit(int id)
		{
			return View(_userRepo.GetById(id));
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

				_userRepo.Save(user);

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
			return View(_userRepo.GetById(id));
		}

		//
		// POST: /User/Delete/5
		
		[HttpPost]
		public ActionResult Delete(int id, User user)
		{
			try
			{
				user = _userRepo.GetById(user.Id);
				_userRepo.Delete(user);
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
