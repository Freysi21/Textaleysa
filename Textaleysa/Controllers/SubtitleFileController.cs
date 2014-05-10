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
		public ActionResult UploadMovieFile(UploadMovieModelView fileInfo, HttpPostedFileBase file)
		{
			if (file != null && fileInfo != null)
			{
				// Create new SubtitleFile
				SubtitleFile f = new SubtitleFile();
				f.language = fileInfo.language;
				// Get the username
				f.userName = User.Identity.Name;
				
				// TODO: FIX ID
				f.ID = 1;


				// Read the whole input file
				StreamReader fileInput = new StreamReader(file.InputStream);
				var line = fileInput.ReadLine();
				while (!string.IsNullOrWhiteSpace(line) || !string.IsNullOrEmpty(line))
				{
					// Create new SubtitleFileChunk 
					SubtitleFileChunk sfc = new SubtitleFileChunk();
					// sfc gets his ID when added to DB
					sfc.subtitleFileID = f.ID;
					sfc.lineID = Convert.ToInt32(line);

					var startString = fileInput.ReadLine().Split(' ');

					TimeSpan startTime = TimeSpan.Parse(startString[0]);
					sfc.startTime = startTime;
 					//startTime.Add(TimeSpan.FromSeconds(3));
					//var stringbla = startTime.ToString();
					TimeSpan stopTime = TimeSpan.Parse(startString[2]);
					sfc.stopTime = stopTime;

					var contentText = fileInput.ReadLine();
					sfc.subtitleLineOne = contentText;
					line = fileInput.ReadLine();
					if (!string.IsNullOrWhiteSpace(line) || !string.IsNullOrEmpty(line))
					{
						sfc.subtitleLineTwo = line;
					}
				}
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