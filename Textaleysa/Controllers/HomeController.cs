using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;



namespace Textaleysa.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About() 
		{ 
			return View();
		}
        [HttpPost]
        public ActionResult About(string title, int year, int season, int episode, HttpPostedFileBase file, ApplicationUser user)
        {
            
            if (file.ContentLength > 0)
            {
                if (year == null)
                {
                    Serie serie = new Serie();
                    serie.season = season;
                    serie.episode = episode;
                    serie.title = title;
                    MediaTitleRepository.Instance.AddMediaTitle(serie);
                }
                else
                {
                    
                }
                string newFile = file.ToString();
                SubtitleFile subs = new SubtitleFile();
                subs.content = newFile;
                subs.userID = Int32.Parse(user.Id);
                subs.date = DateTime.Now;
                //vantar language
                subs.downloadCount = 0;
                SubtitleFileRepository.Instance.AddFile(subs);
            }
            return RedirectToAction("Index");  
        }

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}