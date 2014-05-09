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
		public ActionResult UploadMovieFile(UploadMovieModelView file)
		{
			if (file != null)
			{
				SubtitleFile f = new SubtitleFile();
				f.language = file.language;
				
				f.userName = User.Identity.Name;

				int contentLength = file.file.ContentLength;
				byte[] byteFile = new byte[contentLength];
				file.file.InputStream.Read(byteFile, 0, contentLength);

				// TODO split in to chuncks.
				SubtitleFileChunk sfc = new SubtitleFileChunk();
				sfc.subtitleFileID = f.ID;
				sfc.subtitleContent = byteFile.ToString();
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