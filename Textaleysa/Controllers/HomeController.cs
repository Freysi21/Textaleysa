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
			ViewBag.Message = "Tekka hvort about message virkar frá HomeController";

			return View();

        }

		public ActionResult Contact()
		{
			ViewBag.Message = "Tekka hvort contact message virkar frá HomeController";

			return View();
		}
	}
}