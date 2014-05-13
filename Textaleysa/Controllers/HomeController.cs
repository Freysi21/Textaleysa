using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;
using Textaleysa.Models.ViewModel;



namespace Textaleysa.Controllers
{
	public class HomeController : Controller
	{
		SubtitleFileRepository subtitleFileRepo = new SubtitleFileRepository();
		MediaTitleRepository meditaTitleRepo = new MediaTitleRepository();
		private MovieContext movieDb = new MovieContext();

		public ActionResult Index()
		{
			var mostPopular = (from m in subtitleFileRepo.GetSubtitles()
							   where m.downloadCount >= 1
							   orderby m.downloadCount descending
							   select m).Take(10);

			List<FileFrontPageList> listPopular = new List<FileFrontPageList>();
			foreach(var item in mostPopular)
			{
				FileFrontPageList popularItem = new FileFrontPageList();
				popularItem.ID = item.ID;

				var title = meditaTitleRepo.GetMovieById(item.mediaTitleID);
				if (title != null)
				{
					popularItem.title = title.title + " (" + title.yearReleased.ToString() + ") " + item.language;
					listPopular.Add(popularItem);
				}
			}
			if (listPopular == null)
			{
				return View();
			}
			return View(listPopular);
		}

		public ActionResult MostPopular()
		{
			var mostPopular = from m in subtitleFileRepo.GetSubtitles()
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
				var m = meditaTitleRepo.GetMovieById(f.mediaTitleID);
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
				return RedirectToAction("Index");
			}

			var movies = from m in meditaTitleRepo.GetMovieTitles()
						 where m.title.ToLower().Contains(s.searchString.ToLower())
						 select m;
			if (movies == null)
			{
				return View("Error");
			}

			List<DisplayMovieView> ldmw = new List<DisplayMovieView>();
			foreach (var m in movies)
			{
				var files = from f in subtitleFileRepo.GetSubtitles()
							where f.mediaTitleID == m.ID
							orderby f.downloadCount descending
							select f;
				foreach (var f in files)
				{
					DisplayMovieView dmw = new DisplayMovieView();
					dmw.title = m.title;
					dmw.yearReleased = m.yearReleased;
					dmw.userName = f.userName;
					dmw.language = f.language;
					dmw.date = f.date;
					dmw.downloadCount = f.downloadCount;
					dmw.ID = f.ID;
					ldmw.Add(dmw);
				}
			}
			var result = from r in ldmw
						 orderby r.downloadCount descending
						 select r;
			return View(result);
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
	}
}