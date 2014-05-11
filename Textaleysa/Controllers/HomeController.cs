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
						 where m.title.Contains(s.searchString)
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
					ldmw.Add(dmw);
				}
			}
			return View(ldmw);
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}