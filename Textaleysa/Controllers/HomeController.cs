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
			ViewBag.Message = "Tekka hvort help message virkar ;)";

			return View();
		}

		public ActionResult Popular()
		{
			ViewBag.Message = "Tekka hvort popular message virkar ;)";

			return View();
		}
		public ActionResult Requests()
		{
			ViewBag.Message = "Tekka hvort requests message virkar ;)";

			return View();
		}

        public ActionResult About()
        {
			ViewBag.Message = "About bitches";

			return View();
        }

		public ActionResult Contact()
		{
			//ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}