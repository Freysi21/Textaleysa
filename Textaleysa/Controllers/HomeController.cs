using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models;

namespace Textaleysa.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public ActionResult About(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                string newFile = file.ToString();
                SubtitleFile subs = new SubtitleFile();
                subs.content = newFile;
                subs.userID = System.Web.HttpContext.Current.User;
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
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