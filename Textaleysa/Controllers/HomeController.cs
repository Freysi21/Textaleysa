using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.DataTransferOpjects;
using Textaleysa.Models.Repositories;
using Textaleysa.Models.ViewModel;
using Textaleysa.Repositories;



namespace Textaleysa.Controllers
{
	public class HomeController : Controller
	{
        private readonly ISubtitleRepository _repo;
        public HomeController(ISubtitleRepository repo)
        {
            _repo = repo;
        }
        public HomeController()
        {
            _repo = new SubtitleFileRepository();
        }
		SubtitleFileRepository subtitleFileRepo = new SubtitleFileRepository();
		MediaTitleRepository meditaTitleRepo = new MediaTitleRepository();
		SubtitleFileTransfer subtitleFileTransfer = new SubtitleFileTransfer();
		MediaTitleTransfer mediaTitleTransfer = new MediaTitleTransfer();
		LanguageRepository langRepo = new LanguageRepository();
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			FrontPageViewModel frontPage = new FrontPageViewModel();

			#region get most popular files
			var mostPopular = (from m in subtitleFileRepo.GetAllSubtitles()
							   where m.downloadCount >= 1
							   orderby m.downloadCount descending
							   select m).Take(10);

			frontPage.mostPopularFiles = new List<ListOfFilesView>();
			foreach(var item in mostPopular)
			{
				ListOfFilesView model = new ListOfFilesView();
				model.ID = item.ID;

				var title = mediaTitleTransfer.GetMediaTitleById(item.mediaTitleID);
				if (title != null)
				{
					if (title.isMovie)
					{
						model.isMovie = true;
						model.title = title.title + " (" + title.yearReleased.ToString() + ") " + item.language;
					}
					else
					{
						model.isMovie = false ;
						model.title = title.title + " s" + title.season + "e" + title.episode + " " + item.language;
					}
					frontPage.mostPopularFiles.Add(model);
				}
			}
			if (frontPage.mostPopularFiles == null)
			{
				return View();
			}
			#endregion

			#region get lates uploaded files
			var latestFiles = (from l in subtitleFileRepo.GetAllSubtitles()
							  orderby l.date descending
							  select l).Take(10);

			frontPage.latestFiles = new List<ListOfFilesView>();
			foreach (var item in latestFiles)
			{
				ListOfFilesView model = new ListOfFilesView();
				model.ID = item.ID;

				var title = mediaTitleTransfer.GetMediaTitleById(item.mediaTitleID);
				if (title != null)
				{
					if (title.isMovie)
					{
						model.title = title.title + " (" + title.yearReleased.ToString() + ") " + item.language;
					}
					else
					{
						model.title = title.title + " s" + title.season + "e" + title.episode + " " + item.language;
					}
					frontPage.latestFiles.Add(model);
				}
			}
			if (frontPage.latestFiles == null)
			{
				return View();
			}
			#endregion

			return View(frontPage);
		}

		public ActionResult MostPopular()
		{
			var mostPopular = from m in subtitleFileRepo.GetAllSubtitles()
							  where m.downloadCount >= 1
							  orderby m.downloadCount descending
							  select m;

			if (mostPopular == null)
			{
				return View();
			}
			List<SearchResultListView> popularList = new List<SearchResultListView>();
			foreach (var f in mostPopular)
			{
				var m = mediaTitleTransfer.GetMediaTitleById(f.mediaTitleID);
				if (m == null)
				{
					return View("Error");
				}
				SearchResultListView dmw = new SearchResultListView();
				if (m.isMovie)
				{
					dmw.title = m.title + " (" + m.yearReleased + ")";
				}
				else
				{
					dmw.title = m.title + " s" + m.season + "e" + m.episode;
				}
				dmw.userName = f.userName;
				dmw.language = f.language;
				dmw.date = f.date;
				dmw.downloadCount = f.downloadCount;
				dmw.ID = f.ID;
				popularList.Add(dmw);
			}
		
			return View(popularList);
		}

		public ActionResult Help()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(SearchViewModel s)
		{
			return RedirectToAction("SearchResult", s);
		}


		[HttpGet]
		public ActionResult SearchResult(SearchViewModel s)
		{
			if (string.IsNullOrEmpty(s.searchString) || string.IsNullOrWhiteSpace(s.searchString))
			{
				SearchResultView model = new SearchResultView();
				model.searchString = s.searchString;
				model.searchResultList = new List<SearchResultListView>();
				var titles = meditaTitleRepo.GetAllMovieTitles();
				if (titles == null)
				{
					return View("Error");
				}

				foreach (var item in titles)
				{
					var files = subtitleFileTransfer.GetSubtitleFilesByMediaTitleId(item.ID);
					foreach (var f in files)
					{
						SearchResultListView result = new SearchResultListView();
						#region putting everything in place for the ModelView
						if (item.isMovie)
						{
							result.isMovie = true;
							result.title = item.title + " (" + item.yearReleased + ")";
						}
						else
						{
							result.isMovie = false;
							result.title = item.title + " s" + item.season +
										   "e" + item.episode;
						}
						result.userName = f.userName;
						result.language = f.language;
						result.date = f.date;
						result.downloadCount = f.downloadCount;
						result.ID = f.ID;
						#endregion

						model.searchResultList.Add(result);
					}
					if (model.searchResultList == null)
					{
						return View("Error");
					}
				}
				return View(model);
			}
			var results = mediaTitleTransfer.SearchAfterTitle(s.searchString);
			if (results == null)
			{
				return View("Error");
			}

			SearchResultView model2 = new SearchResultView();
			model2.searchString = s.searchString;
			model2.searchResultList = new List<SearchResultListView>();
			foreach (var item in results)
			{
				var files = subtitleFileTransfer.GetSubtitleFilesByMediaTitleId(item.ID);

				foreach (var f in files)
				{
					SearchResultListView result = new SearchResultListView();
					#region putting everything in place for the ModelView
					if (item.isMovie)
					{
						result.isMovie = true;
						result.title = item.title + " (" + item.yearReleased + ")";
					}
					else
					{
						result.isMovie = false;
						result.title = item.title + " s" + item.season +
									   "e" + item.episode;
					}
					result.userName = f.userName;
					result.language = f.language;
					result.date = f.date;
					result.downloadCount = f.downloadCount;
					result.ID = f.ID;
					#endregion

					model2.searchResultList.Add(result);
				}
				if (model2.searchResultList == null)
				{
					return View("Error");
				}
			}
			return View(model2);
		}

		public ActionResult Popular()
		{
			ViewBag.Message = "Tekka hvort popular message virkar frá HomeController";

			return View();
		}

        public ActionResult About()
        {
			return View();
        }

		public ActionResult Contact()
		{
			ViewBag.Message = "Tekka hvort contact message virkar frá HomeController";

			return View();
		}

		public ActionResult Error()
		{
			return View("Error");
		}
	}

}