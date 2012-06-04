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
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Capt.Helpers;
using Capt.Models;
using Capt.Services;

namespace Capt.Controllers
{
	/// <summary>
	/// The controller for the CaptionForPictureViewModel.
	/// </summary>
	public class CaptionForPictureController : Controller
    {
		/// <summary>
		/// Our account service we'll be a-usin'.
		/// </summary>
		private IAccountService _accountService;

		/// <summary>
		/// Our picture service this controller will use
		/// </summary>
		private IPictureService _pictureService;

		/// <summary>
		/// Constructor which allows you to specify your own services.
		/// </summary>
		/// <param name="accountService">The account service to use for this controller</param>
		/// <param name="pictureService">The picture service to use for this controller</param>
		public CaptionForPictureController(IAccountService accountService, IPictureService pictureService)
		{
			_accountService = accountService;
			_pictureService = pictureService;
		}

		/// <summary>
		/// Zero-argument constructor will choose the most appropriate repositories for this environment.
		/// </summary>
		public CaptionForPictureController()
			: this(new AccountService(), new PictureService())
		{
		}

		/// <summary>
		/// The action for the RSS "Captions" RSS feed.
		/// </summary>
		/// <returns></returns>
		[OutputCache(Duration = 3600)]
		public RssActionResult IndexRss()
		{
			List<SyndicationItem> items = new List<SyndicationItem>();

			var captions = _pictureService.GetAllCaptions(false, 10);

			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);

			string urlPrefix = "http://" + Request.Url.Host;
			if (!Request.Url.IsDefaultPort)
			{
				urlPrefix += ":" + Request.Url.Port;
			}

			var baseUri = new Uri(urlPrefix + urlHelper.Action("Index", "CaptionForPicture"));


			foreach (var caption in captions)
			{
				var uri = new Uri(urlPrefix + urlHelper.Action("Create", "PictureCaptions", new { pictureId = caption.Picture.Id }));
				var authorUrl = urlPrefix + urlHelper.Action("IndexByUser", "CaptionForPicture", new { userName = caption.User.Name });

				string html = String.Format(
					@"<div style=""text-align: center; padding-bottom: 15px;"">
						<div style=""margin-bottom: 10px; text-align: left; display: inline-block; background-color: #F1F1F1; padding: 5px; zoom:1; display:inline;"">
							<a href=""{5}""><img style=""border: none;"" src=""{0}"" /></a><br />
							<a href=""{3}""><img src=""{4}"" /></a>
							<a href=""{1}"">{2}</a>
						</div>
						<br /><span style=""line-height: 150%; font-size: 200%;"">{6}</span>
						<br /><span>&mdash; {7}</span>
						<br /><a style=""line-height: 150%; font-size: 200%;"" href=""{5}"">Got a good caption for this?</a>
					</div>",
					caption.Picture.Url, // 0
					caption.Picture.AttributionUrl, // 1
					caption.Picture.Attribution, // 2
					caption.Picture.License.InfoUrl, // 3
					caption.Picture.License.ImageUrl, // 4
					uri.ToString(), // 5
					caption.Text, // 6
					caption.User.Name != null && !caption.IsAnonymous ? 
						String.Format(@"<a href=""{0}"">{1}</a>", authorUrl, caption.User.Name) : "Anonymous" // 7
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
					LastUpdatedTime = caption.Event.Datetime,
					PublishDate = caption.Event.Datetime,
					Title = new TextSyndicationContent(
						caption.Text,
						TextSyndicationContentKind.Plaintext)
				};

				items.Add(item);

			}

			SyndicationFeed feed = new SyndicationFeed()
			{
				BaseUri = baseUri,
				Items = items,
				LastUpdatedTime = captions.First().Event.Datetime,
				Title = new TextSyndicationContent("Latest Captions @ " + ViewBag.SiteName, TextSyndicationContentKind.Plaintext)
			};


			return new RssActionResult() { Feed = feed };
		}

		/// <summary>
		/// Show a list of all the captions for pictures.  This is used for the "Latest captions" page.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			var captions = _pictureService.GetAllCaptions(ViewBag.IsAdminStuffShown, null);
			List<CaptionForPictureViewModel> cfpvms = new List<CaptionForPictureViewModel>();

			foreach (var caption in captions)
			{
				cfpvms.Add(new CaptionForPictureViewModel(caption));
			}

			User viewingUser = Session["User"] as User;
			ViewBag.IsUserBelowThreshold = (viewingUser != null && viewingUser.Score < 0);

			ViewBag.ArePermalinksShown = true;
			ViewBag.AreMoreCaptionsLinksShown = true;
			ViewBag.AreCaptionAuthorLinksShown = true;

			DateTime startOfWeek = DateTime.Today.AddDays(DateTime.Today.DayOfWeek - DayOfWeek.Monday);
			DateTime endOfWeek = startOfWeek.AddDays(6);

			DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DateTime endOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);

			ViewBag.RankedUsers = new Dictionary<string, List<RankedUser>>() {
				{ "today" , _accountService.GetRankedUsers(DateTime.Today, DateTime.Today.AddDays(1)) },
				{ "thisWeek", _accountService.GetRankedUsers(startOfWeek, endOfWeek) },
				{ "thisMonth", _accountService.GetRankedUsers(startOfMonth, endOfMonth) },
				{ "allTime", _accountService.GetRankedUsers() }
			};

			return View(cfpvms.OrderByDescending(c => c.Created));
		}

		/// <summary>
		/// Cat all the Captions for Pictures for a given user
		/// </summary>
		/// <param name="userName">The username (or numeric ID) whose captions you want to display</param>
		/// <returns></returns>
        public ActionResult IndexByUser(string userName)
        {
			List<CaptionForPictureViewModel> cfpvms = new List<CaptionForPictureViewModel>();

			User userToView;

			int userId = -1;
			if (int.TryParse(userName, out userId))
			{
				userToView = _accountService.GetUserById(userId);
			}
			else
			{
				userToView = _accountService.GetUserByName(userName);
			}

			if (userToView == null || (!ViewBag.IsAdminStuffShown && userToView.IsLocked))
			{
				throw new HttpException(404, "No user with name " + userName + " found.");
			}

			foreach (var caption in userToView.Captions)
			{
				if(
					!ViewBag.IsAdminStuffShown
					&& (
						!caption.IsVisible || !caption.Picture.IsVisible || caption.Picture.Activates > DateTime.UtcNow
						|| caption.Picture.IsPrivate || caption.IsAnonymous
						|| (caption.Picture.User != null && caption.Picture.User.IsLocked)
					)
				)
				{
					continue;
				}

				cfpvms.Add(new CaptionForPictureViewModel(caption));
			}


			User viewingUser = Session["User"] as User;
			ViewBag.IsUserBelowThreshold = (viewingUser != null && viewingUser.Score < 0);

			ViewBag.ArePermalinksShown = true;
			ViewBag.AreMoreCaptionsLinksShown = true;
			ViewBag.AreCaptionAuthorLinksShown = false;
			ViewBag.UserName = (!String.IsNullOrWhiteSpace(userToView.Name) ? userToView.Name : "User " + userToView.Id);

			return View(cfpvms.OrderByDescending(c => c.Created));
        }

    }
}
