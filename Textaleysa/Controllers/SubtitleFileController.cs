using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;
using Textaleysa.Models.ViewModel;

namespace Textaleysa.Controllers
{
    public class SubtitleFileController : Controller
    {
		private CommentContext db = new CommentContext();
		SubtitleFileRepository subtitleFileRepo = new SubtitleFileRepository();

		private MovieContext movieDb = new MovieContext();
		MediaTitleRepository meditaTitleRepo = new MediaTitleRepository();

		private SubtitleFileChunkContext chunkDb = new SubtitleFileChunkContext();

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
				var movie = meditaTitleRepo.GetMovie(fileInfo.title);
				if (movie == null)
				{
					Movie m = new Movie();
					m.title = fileInfo.title;
					m.yearReleased = fileInfo.yearReleased;
					// TODo : add movietitle to db
					meditaTitleRepo.AddMediaTitle(m);
					f.mediaTitleID = m.ID;
				}
				else 
				{
					f.mediaTitleID = movie.ID;
				}

				f.language = fileInfo.language;
				// Get the username
				f.userName = User.Identity.Name;
				
				// Add the subtitleFile in DB
				subtitleFileRepo.AddSubtitleFile(f);


				// Read the whole input file
				StreamReader fileInput = new StreamReader(file.InputStream);
				do
				{
					SubtitleFileChunk sfc = new SubtitleFileChunk();
					// sfc gets his ID when added to DB
					sfc.subtitleFileID = f.ID;

					// Read the first line of SubtitleFileChunk which is the ID of SubtitleFileChunk
					var line = fileInput.ReadLine(); 
					sfc.lineID = Convert.ToInt32(line);

					// Split the Subtitle time in to 3 parts (ex. line2[0] = "00:00:55,573" 
					// line2[1] = "-->" line2[2] = "00:00:58,867")
					var line2 = fileInput.ReadLine().Split(' ');
					// TimeSpan startTime = TimeSpan.Parse(line2[0]);
					sfc.startTime = line2[0];
					//TimeSpan stopTime = TimeSpan.Parse(line2[2]);
					sfc.stopTime = line2[2];

					// Read the first line of text in the subtitlechunk
					var line3 = fileInput.ReadLine(); 
					sfc.subtitleLine1 = line3;

					// Read the second line of text but if the line is empty
					var line4 = fileInput.ReadLine(); 
					if(!string.IsNullOrEmpty(line4))
					{
						sfc.subtitleLine2 = line4;
						var line5 = fileInput.ReadLine(); // "" 

						if(!string.IsNullOrEmpty(line5))
						{
							sfc.subtitleLine3 = line5;
							fileInput.ReadLine();
						}
						else
						{
							sfc.subtitleLine3 = null;
						}
					}
					else
					{
						sfc.subtitleLine2 = null;
						sfc.subtitleLine3 = null;
					}
					subtitleFileRepo.AddSubtitleChunk(sfc);

				} while(!fileInput.EndOfStream);
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