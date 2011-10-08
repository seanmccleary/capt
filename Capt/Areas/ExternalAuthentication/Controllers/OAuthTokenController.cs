using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBasics.Areas.ExternalAuthentication.Controllers
{
    public class OAuthTokenController : Controller
    {
        //
        // GET: /ExternalAuthentication/OAuthToken/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /ExternalAuthentication/OAuthToken/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ExternalAuthentication/OAuthToken/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ExternalAuthentication/OAuthToken/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ExternalAuthentication/OAuthToken/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ExternalAuthentication/OAuthToken/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ExternalAuthentication/OAuthToken/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ExternalAuthentication/OAuthToken/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
