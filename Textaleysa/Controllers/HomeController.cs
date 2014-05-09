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

		public ActionResult Help()
		{
			ViewBag.Message = "Tekka hvort help message virkar frá HomeController";

			return View();
		}

		public ActionResult Popular()
		{
			ViewBag.Message = "Tekka hvort popular message virkar frá HomeController";

			return View();
		}
		public ActionResult Requests()
		{
			ViewBag.Message = "Tekka hvort requests message virkar frá HomeController";

			return View();
		}

        public ActionResult About()
        {
<<<<<<< HEAD
			ViewBag.Message = "Tekka hvort about message virkar frá HomeController";

			return View();
=======
            
            if (file.ContentLength > 0)
            {
                if (year == 0)
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
>>>>>>> 77cbce21d2fa77e9ccc2ba6da1dec9bf0b889352
        }

		public ActionResult Contact()
		{
			ViewBag.Message = "Tekka hvort contact message virkar frá HomeController";

			return View();
		}
	}
}