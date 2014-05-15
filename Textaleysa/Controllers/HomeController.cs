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



namespace Textaleysa.Controllers
{
	public class HomeController : Controller
	{
		SubtitleFileRepository subtitleFileRepo = new SubtitleFileRepository();
		MediaTitleRepository meditaTitleRepo = new MediaTitleRepository();
		SubtitleFileTransfer subtitleFileTransfer = new SubtitleFileTransfer();
		MediaTitleTransfer mediaTitleTransfer = new MediaTitleTransfer();
		LanguageRepository langRepo = new LanguageRepository();
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			FrontPageViewModel frontPage = new FrontPageViewModel();
			#region get languages for dropdownlist
			frontPage.languageOptions = langRepo.GetLanguages();
			if (frontPage.languageOptions == null)
			{
				return View("Error");
			}
			#endregion

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
			List<DisplayMovieView> popularList = new List<DisplayMovieView>();
			foreach (var f in mostPopular)
			{
				var m = mediaTitleTransfer.GetMovieById(f.mediaTitleID);
				if (m == null)
				{
					return View("Error");
				}
				DisplayMovieView dmw = new DisplayMovieView();
				dmw.title = m.title;
				dmw.yearReleased = m.yearReleased;
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
				List<DisplayMovieView> modelList = new List<DisplayMovieView>();
				var titles = meditaTitleRepo.GetAllMovieTitles();
				if (titles == null)
				{
					return View("Error");
				}

				foreach (var item in titles)
				{
					var files = subtitleFileRepo.GetAllSubtitles();
					foreach (var f in files)
					{
						DisplayMovieView dmw = new DisplayMovieView();
						#region putting everything in place for the ModelView
						dmw.title = item.title;
						dmw.yearReleased = item.yearReleased;
						dmw.userName = f.userName;
						dmw.language = f.language;
						dmw.date = f.date;
						dmw.downloadCount = f.downloadCount;
						dmw.ID = f.ID;
						#endregion

						modelList.Add(dmw);
					}
					if (modelList == null)
					{
						return View("Error");
					}
				}
				return View(modelList);
			}
			var results = mediaTitleTransfer.SearchAfterTitle(s.searchString);
			if (results == null)
			{
				return View("Error");
			}

			List<DisplayMovieView> modelList2 = new List<DisplayMovieView>();
			foreach (var item in results)
			{
				var files = subtitleFileTransfer.GetSubtitleFilesByMediaTitleId(item.ID);

				foreach (var f in files)
				{
					DisplayMovieView dmw = new DisplayMovieView();
					#region putting everything in place for the ModelView
					dmw.title = item.title;
					dmw.yearReleased = item.yearReleased;
					dmw.userName = f.userName;
					dmw.language = f.language;
					dmw.date = f.date;
					dmw.downloadCount = f.downloadCount;
					dmw.ID = f.ID;
					#endregion

					modelList2.Add(dmw);
				}
				if (modelList2 == null)
				{
					return View("Error");
				}
			}
			return View(modelList2);
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