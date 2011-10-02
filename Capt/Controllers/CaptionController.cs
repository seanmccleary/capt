using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capt.Models;

namespace Capt.Controllers
{

	/// <summary>
	/// A very basic, scaffolded controller for the Caption model.  This is only used for ugly admin stuff, not for
	/// anything user-facing.
	/// </summary>
	[Authorize(Roles = "Administrator")]
    public class CaptionController : ApplicationController
    {

		private ICaptionRepository _captionRepo;

		public CaptionController(ICaptionRepository captionrepo)
		{
			_captionRepo = captionrepo;
		}

		public CaptionController()
			: this(new Capt.Models.LinqToMySql.CaptionRepository())
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
            return View(_captionRepo.GetById(id));
        }

        
        //
        // GET: /Caption/Edit/5
 
        public ActionResult Edit(int id)
        {
			return View(_captionRepo.GetById(id));
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
				
				_captionRepo.Save(caption);

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
            return View(_captionRepo.GetById(id));
        }

        //
        // POST: /Caption/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Caption caption)
        {
            try
            {
				caption = _captionRepo.GetById(id);
				int pictureId = caption.PictureId;
				_captionRepo.Delete(caption);
 
                return RedirectToAction("Create", "PictureCaptions", new { pictureId = pictureId });
            }
            catch
            {
                return View();
            }
        }
    }
}
