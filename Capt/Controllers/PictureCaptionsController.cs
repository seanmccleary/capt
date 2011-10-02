﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Capt.Helpers;
using Capt.Models;

namespace Capt.Controllers
{
	/// <summary>
	/// Actions related to the PictureCaption view model.
	/// </summary>
    public class PictureCaptionsController : ApplicationController
    {

		private IPictureRepository _pictureRepo;

		private ICaptionRepository _captionRepo;

		private IUserRepository _userRepo;

		/// <summary>
		/// These are the options that should be shown in the drop-down list to let users
		/// choose how they'd like to sort the captions shown to them.
		/// </summary>
		private Dictionary<string, SelectListItem> _sortOptions = new Dictionary<string, SelectListItem>() 
			{
				{ "newest", new SelectListItem { Text = "Show newest captions first", Value = "newest" } },
				{ "best", new SelectListItem { Text = "Show highest-rated captions first", Value = "best" } },
				{ "oldest", new SelectListItem { Text = "Show oldest captions first", Value = "oldest" } },
				{ "worst", new SelectListItem { Text = "Show lowest-rated captions first", Value = "worst" } }
			};

		/// <summary>
		/// Constructor which allows you to specify model repositories.
		/// </summary>
		/// <param name="pictureRepo"></param>
		/// <param name="captionRepo"></param>
		/// <param name="userRepo"></param>
		public PictureCaptionsController(IPictureRepository pictureRepo, ICaptionRepository captionRepo, IUserRepository userRepo)
		{
			_pictureRepo = pictureRepo;
			_captionRepo = captionRepo;
			_userRepo = userRepo;

			// Set the default sort options
			string sortPref = System.Web.HttpContext.Current.Session["captionSortOrder"] as string;
			if (sortPref != null && _sortOptions.ContainsKey(sortPref))
			{
				_sortOptions[sortPref].Selected = true;
			}
			ViewBag.SortOptions = _sortOptions.Values.ToList();

			
			User user = System.Web.HttpContext.Current.Session["User"] as User;
			ViewBag.IsUserBelowThreshold = (user != null && user.Score < 0);
			ViewBag.ArePermalinksShown = true;
			ViewBag.AreMoreCaptionsLinksShown = false;
			ViewBag.AreCaptionAuthorLinksShown = true;
		}

		/// <summary>
		/// Constructor which will use the most appropriate repositories for you.
		/// </summary>
		public PictureCaptionsController()
			: this(
				new Capt.Models.LinqToMySql.PictureRepository(),
				new Capt.Models.LinqToMySql.CaptionRepository(),
				new Capt.Models.LinqToMySql.UserRepository()
			)
		{
		}

		/// <summary>
		/// For when the user goes to the page to create a caption.
		/// </summary>
		/// <param name="pictureId">ID of the picture for which he's creating a caption</param>
		/// <returns></returns>
		public ActionResult Create(int pictureId)
		{
			Picture pic = _pictureRepo.GetById(pictureId);

			if (
				pic == null
				|| (
					!ViewBag.IsAdminStuffShown 
					&& (
						pic.Activates > DateTime.UtcNow || !pic.IsVisible || (pic.User != null && pic.User.IsLocked)
					)
				)
			)
			{
				throw new HttpException(404, "Picture #" + pictureId + " doesn't seem to exist.");
			}

			PictureCaptionsViewModel pcvm = new PictureCaptionsViewModel(pic, ViewBag.IsAdminStuffShown);

			User user = Session["User"] as User;
			if (user != null && !String.IsNullOrWhiteSpace(user.Name))
			{
				pcvm.UserName = user.Name;
			}

			ViewBag.HideSecondaryControls = true;
			ViewBag.IsCaptionTextAreaHinted = true;
			ViewBag.IsCaptionTextAreaExpanded = false;
			ViewBag.IsUserNameHinted = true;
			SetNextAndPreviousPictureIds(pic);

			
			return View(pcvm);
		}


		/// <summary>
		/// For when the user submits the page to create a caption
		/// </summary>
		/// <param name="pictureId">ID of the picture for which he's adding a caption</param>
		/// <param name="pcvm">The PictureCaptionViewModel with the newly-created caption</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		[ValidateInput(false)]
		public ActionResult Create(int pictureId, PictureCaptionsViewModel pcvm)
		{
			Picture pic = _pictureRepo.GetById(pictureId);

			pcvm.SetValuesFromPicture(pic, ViewBag.IsAdminStuffShown);

			ViewBag.HideSecondaryControls = false;

			ViewBag.IsCaptionTextAreaHinted = pcvm.IsCaptionTextHinted;
			ViewBag.IsCaptionTextAreaExpanded = true;
			ViewBag.IsUserNameHinted = pcvm.IsUserNameHinted;
			SetNextAndPreviousPictureIds(pic);

			try
			{

				if (!ModelState.IsValid)
				{
					return View(pcvm);
				}

				Caption caption = new Caption()
				{
					Text = pcvm.CaptionText,
					IsVisible = true,
					IsAnonymous = pcvm.IsAnonymous,
					User = Session["User"] as User,
					PictureId = pictureId,
					Event = new Event(EventType.CaptionCreated)
				};

				if (
					!caption.IsAnonymous
					&& String.IsNullOrWhiteSpace(caption.User.Name)
					&& !String.IsNullOrWhiteSpace(pcvm.UserName))
				{
					caption.User.Name = pcvm.UserName;
					_userRepo.Save(caption.User);
				}

				_captionRepo.Save(caption);

				// Providing the route values here seems to be necessary for mono
				// but not MS... interesting.
				return RedirectToAction("Create", new { pictureId = pictureId });
			}
			catch
			{
				return View(pcvm);
			}
		}

		/// <summary>
		/// Set the "Next" and "previous" picture IDs in the ViewBag; used for the paging controls.
		/// TODO: Make this an action filter.
		/// </summary>
		/// <param name="pic">The picture the user is looking at right now</param>
		private void SetNextAndPreviousPictureIds(Picture pic)
		{
			Picture nextPic = _pictureRepo.GetNext(pic);
			Picture previousPic = _pictureRepo.GetPrevious(pic);

			if (previousPic != null && (ViewBag.IsAdminStuffShown || previousPic.Activates < DateTime.UtcNow))
			{
				ViewBag.PreviousPictureId = previousPic.Id;
			}
			else
			{
				ViewBag.PreviousPictureId = 0;
			}

			if (nextPic != null && (ViewBag.IsAdminStuffShown || nextPic.Activates < DateTime.UtcNow))
			{
				ViewBag.NextPictureId = nextPic.Id;
			}
			else
			{
				ViewBag.NextPictureId = 0;
			}

		}

		/// <summary>
		/// Return the RSS feed for latest pictures.
		/// </summary>
		/// <returns></returns>
		[OutputCache(Duration = 3600)]
		public RssActionResult IndexRss()
		{

			List<SyndicationItem> items = new List<SyndicationItem>();

			var pictures = (from p in _pictureRepo.GetAll()
						   where p.Activates < DateTime.UtcNow
						   && p.IsVisible && !p.IsPrivate
						   && (p.User == null || !p.User.IsLocked)
						   orderby p.Activates descending
						   select p).Take(10);

			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);

			string urlPrefix = "http://" + Request.Url.Host;
			if (!Request.Url.IsDefaultPort)
			{
				urlPrefix += ":" + Request.Url.Port;
			}

			var baseUri = new Uri(urlPrefix + urlHelper.Action("Index", "PictureCaptions"));

			foreach (var picture in pictures)
			{

				var pictureEvent = picture.Captions.OrderByDescending(c => c.Event.Datetime).FirstOrDefault();

				var uri = new Uri(urlPrefix + urlHelper.Action("Create", "PictureCaptions", new { pictureId = picture.Id }));

				string html = String.Format(
					@"<div style=""text-align: center; padding-bottom: 15px;"">
						<div style=""margin-bottom: 10px; text-align: left; display: inline-block; background-color: #F1F1F1; padding: 5px; zoom:1; display:inline;"">
							<a href=""{5}""><img style=""border: none;"" src=""{0}"" /></a><br />
							<a href=""{3}""><img src=""{4}"" /></a>
							<a href=""{1}"">{2}</a>
						</div>
						<br/><a style=""font-size: 200%;"" href=""{5}"">Got a good caption for this?</a>
					</div>",
					picture.Url, // 0
					picture.AttributionUrl, // 1
					picture.Attribution, // 2
					picture.License.InfoUrl, // 3
					picture.License.ImageUrl, // 4
					uri.ToString() // 3

					);

				var content = new TextSyndicationContent(html, TextSyndicationContentKind.Html);

				SyndicationItem item = new SyndicationItem()
				{
					BaseUri = uri,
					Content = content,
					Copyright = new TextSyndicationContent(
						"Picture and caption copyrights are held by their original authors.", 
						TextSyndicationContentKind.Plaintext),
					Id = uri.ToString(),
					LastUpdatedTime = (pictureEvent != null ? pictureEvent.Event.Datetime : picture.Activates),
					PublishDate = picture.Activates,
					Title = new TextSyndicationContent(
						picture.Activates.ToString(),
						TextSyndicationContentKind.Plaintext)
				};

				items.Add(item);
			}


			SyndicationFeed feed = new SyndicationFeed()
			{
				BaseUri = baseUri,
				Items = items,
				LastUpdatedTime = pictures.First().Activates,
				Title = new TextSyndicationContent("Latest Pictures @ " + ViewBag.SiteName, TextSyndicationContentKind.Plaintext)
			};
				
			return new RssActionResult() { Feed = feed };

		}

		/// <summary>
		/// Show the list of pictures foer the home page. 
		/// </summary>
		/// <param name="start">Where in the list to start. (i.e. if showing 10 results per page, and this is page 3, set it to 20)</param>
		/// <returns></returns>
		public ActionResult Index(int start = 0)
		{

			var pictures = _pictureRepo.GetRanked(start, 11, (bool) ViewData["IsAdminStuffShown"]);

			ViewBag.IsNextPageAvailable = pictures.Count() == 11;
			ViewBag.IsPreviousPageAvailable = start != 0;
			ViewBag.NextPageStartsAt = start + 10;
			ViewBag.PreviousPageStartsAt = start - 10;
			
			// We only got that extra one to see if there was another page of results
			pictures = pictures.Take(10);

			// Get the next-to-activeate picture
			var nextPicture = (from p in _pictureRepo.GetAll()
							   where p.Activates > DateTime.UtcNow
							   orderby p.Activates
							   select p).FirstOrDefault();

			ViewBag.NextPictureActivates = (nextPicture != null ? nextPicture.Activates.ToString() + " UTC-0000" : "");

			ViewBag.RankedUsers = (from u in _userRepo.GetAll()
								   where !u.IsLocked
								   && !String.IsNullOrWhiteSpace(u.Name)
								   select u).OrderByDescending(u => u.Score).Take(100);

			return View(from p in pictures select new PictureCaptionsViewModel(p, ViewBag.IsAdminStuffShown));
		}


		/// <summary>
		/// Used to set the sort order for the caption list below
		/// </summary>
		/// <param name="pictureId"></param>
		/// <param name="sortOrder"></param>
		/// <returns></returns>
		public ActionResult SetSortOrder(int pictureId, string sortOrder)
		{

			Session["captionSortOrder"] = sortOrder;

			return RedirectToAction("Create", new { pictureId = pictureId });
		}

		/// <summary>
		/// Show the caption details for one of the permalinks
		/// </summary>
		/// <param name="captionId"></param>
		/// <returns></returns>
		public ActionResult CaptionDetails(int captionId)
		{

			Caption caption = _captionRepo.GetById(captionId);

			if (
				caption == null
				|| caption.Picture == null
				|| (
					!ViewBag.IsAdminStuffShown
					&& (
						(
							caption.Picture.Activates > DateTime.UtcNow || !caption.Picture.IsVisible || (caption.Picture.User != null && caption.Picture.User.IsLocked)
						)
						|| (
							!caption.IsVisible || (caption.User.IsLocked)
						)
					)
				)
			)
			{
				throw new HttpException(404, "Caption #" + captionId + " doesn't seem to exist.");
			}


			PictureCaptionsViewModel pcvm = new PictureCaptionsViewModel(caption.Picture, ViewBag.IsAdminStuffShown);

			ViewBag.ArePermalinksShown = false;
			ViewBag.AreMoreCaptionsLinksShown = true;

			// We only really want to display the caption from the parameter
			// TODO: Change this so it doesnt display an IEnumerable with one element. That's lame.
			pcvm.Captions = pcvm.Captions.Where(c => c.Id == captionId);
			return View(pcvm);

		}

    }
}