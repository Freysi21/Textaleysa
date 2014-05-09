using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models;

namespace Textaleysa.Controllers
{
    public class SubtitleFileController : Controller
    {
		private HRContext db = new HRContext();

        // GET: /SubtitleFile/
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult UploadFile()
		{
			return View();
		}

		// TODO: Add autorized !
		[HttpPost]
		public ActionResult UploadFile(SubtitleFile file)
		{
			SubtitleFile f = new SubtitleFile();
			f.language = file.language;
			f.downloadCount = 0;
			f.userID = User.Identity.Name;
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}