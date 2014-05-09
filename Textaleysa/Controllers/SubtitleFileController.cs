using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.ViewModel;

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

		public ActionResult UploadMovieFile()
		{
			return View();
		}

		// TODO: Add authorized !
		[HttpPost]
		public ActionResult UploadMovieFile(UploadMovieModelView file, HttpPostedFileBase test)
		{
			if (file != null)
			{
				SubtitleFile f = new SubtitleFile();
				f.language = file.language;
				f.userName = User.Identity.Name;
				f.ID = 1;

				StreamReader asdf = new StreamReader(test.InputStream);
				var line = asdf.ReadLine();
				if (!string.IsNullOrWhiteSpace(line))
				{
					SubtitleFileChunk sfc = new SubtitleFileChunk();
					// sfc gets his ID when added to DB
					sfc.subtitleFileID = f.ID;
					sfc.lineID = Convert.ToInt32(line);

					var startString = asdf.ReadLine().Split(' ');

					TimeSpan startTime = TimeSpan.Parse(startString[0]);
					sfc.startTime = startTime;
 					startTime.Add(TimeSpan.FromSeconds(3));
					var stringbla = startTime.ToString();

					DateTime stopTime = DateTime.ParseExact(startString[2], "HH:mm:ss,fff",
					System.Globalization.CultureInfo.InvariantCulture);
					
					//sfc.stopTime = stopTime;

				}
				// TODO split in to chuncks.
			}
			
			return RedirectToAction("UploadMovieFile");
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