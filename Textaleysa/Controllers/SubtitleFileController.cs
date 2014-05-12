using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

		private LanguageRepository langDb = new LanguageRepository();

        // GET: /SubtitleFile/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult DisplayFile(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}
			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);

			if (subtitleFile == null)
			{
				return View("Error");
			}

			var movie = meditaTitleRepo.GetMovieById(subtitleFile.mediaTitleID);

			if (movie == null)
			{
				return View("Error");
			}
			else
			{
				DisplayMovieView dmv = new DisplayMovieView();
				dmv.ID = subtitleFile.ID;
				dmv.title = movie.title;
				dmv.yearReleased = movie.yearReleased;
				dmv.grade = 10;
				dmv.userName = subtitleFile.userName;
				dmv.language = subtitleFile.language;
				dmv.date = subtitleFile.date;
				dmv.downloadCount = subtitleFile.downloadCount;
				return View(dmv);
			}
			
		}

		public FileStreamResult DownloadFile(int? id)
		{
			if (id == null)
			{
				return null;
			}

			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return null;
			}
			var movie = meditaTitleRepo.GetMovieById(subtitleFile.mediaTitleID);
			if (movie == null)
			{
				return null;
			}

			var subtitleFileChunks = from c in subtitleFileRepo.GetSubtitleFileChunks()
									 where c.subtitleFileID == id
									 orderby c.lineID ascending
									 select c;

			var result = "";
			foreach (var item in subtitleFileChunks)
			{
				result += item.lineID.ToString();
				result += Environment.NewLine;

				result += (item.startTime + " --> ");
				result += item.stopTime;
				result += Environment.NewLine;
				result += item.subtitleLine1;
				result += Environment.NewLine;
				if (item.subtitleLine2 != null)
				{
					result += item.subtitleLine2;
					result += Environment.NewLine;
					if (item.subtitleLine3 != null)
					{
						result += item.subtitleLine3;
						result += Environment.NewLine;
					}
				}
				result += Environment.NewLine;
			}

			var byteArray = Encoding.UTF8.GetBytes(result);
			//var byteArray = Encoding.ASCII.GetBytes(result);
			var stream = new MemoryStream(byteArray);

			var fileTitle = "";
			fileTitle += (movie.title + " " + movie.yearReleased.ToString() + " " + subtitleFile.language + ".srt");
			
			subtitleFile.downloadCount++;
			subtitleFileRepo.ModifySubtitleFile(subtitleFile);
			//return File(stream, "text/plain", fileTitle);
			return File(stream, "charset=\"utf-8\"", fileTitle);
		}

		public ActionResult UploadMovieFile()
		{
			UploadMovieModelView u = new UploadMovieModelView();
			u.languageOptions = langDb.GetLanguages();
			if (u == null)
			{
				return View("Error");
			}
			return View(u);
		}

		// TODO: Add authorized !
		[HttpPost]
		public ActionResult UploadMovieFile(UploadMovieModelView fileInfo, HttpPostedFileBase file)
		{
			if (file != null && fileInfo != null)
			{
				// Create new SubtitleFile
				SubtitleFile f = new SubtitleFile();
				var movie = (from m in meditaTitleRepo.GetMovieTitles()
							 where m.title == fileInfo.title
							 select m).FirstOrDefault();

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

				var lan = langDb.GetLanguageById(fileInfo.languageID);
				f.language = lan.language;
				// Get the username
				f.userName = User.Identity.Name;

				// Add the subtitleFile in DB
				subtitleFileRepo.AddSubtitleFile(f);

				try
				{
					// Read the whole input file
					StreamReader fileInput = new StreamReader(file.InputStream, System.Text.Encoding.UTF8, true);

					do
					{
						#region uploading the file
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
						if (!string.IsNullOrEmpty(line4))
						{
							sfc.subtitleLine2 = line4;
							var line5 = fileInput.ReadLine(); // "" 

							if (!string.IsNullOrEmpty(line5))
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

					} while (!fileInput.EndOfStream);
					int? ID = f.ID;

					return RedirectToAction("DisplayFile", new { id = ID });
						#endregion
				}
				catch (Exception)
				{
					subtitleFileRepo.DeleteSubtitleFileChunk(f.ID);
					subtitleFileRepo.DeleteSubtitleFile(f);
					return View("Error");
				}
			}	
			
			return RedirectToAction("UploadMovie");
		}

		public ActionResult DeleteFile(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}
			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}

			subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
			subtitleFileRepo.DeleteSubtitleFile(subtitleFile);

			return View("Index");
		}

		public ActionResult AddLanguageChoise()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddLanguageChoise(LanguageView l)
		{
			Language lang = new Language();
			lang.language = l.language;
			langDb.AddLanguage(lang);
			return RedirectToAction("UploadMovieFile");
		}

		public ActionResult EditFile(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}
			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}

			var chunks = from c in subtitleFileRepo.GetSubtitleFileChunks()
						 where c.subtitleFileID == subtitleFile.ID
						 select c;
			
			string result = "";

			foreach (var item in chunks)
			{
				result += item.lineID.ToString();
				result += Environment.NewLine;

				result += (item.startTime + " --> ");
				result += item.stopTime;
				result += Environment.NewLine;
				result += item.subtitleLine1;
				result += Environment.NewLine;
				if (item.subtitleLine2 != null)
				{
					result += item.subtitleLine2;
					result += Environment.NewLine;
					if (item.subtitleLine3 != null)
					{
						result += item.subtitleLine3;
						result += Environment.NewLine;
					}
				}
				result += Environment.NewLine;
			}

			EditFileView file = new EditFileView();
			file.content = result;
			file.ID = subtitleFile.ID;
			return View(file);
		}

		[HttpPost]
		public ActionResult EditFile(EditFileView file)
		{
			if (file.ID == null)
			{
				return View("Error");
			}
			var subtitleFile = subtitleFileRepo.GetSubtitleById(file.ID);
			if (subtitleFile == null)
			{
				return View("Error");
			}
			SubtitleFile newFile = new SubtitleFile();
			newFile = subtitleFile;

			subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
			subtitleFileRepo.DeleteSubtitleFile(subtitleFile);

			//StringReader fileInput = new StringReader(file.content);
			StreamReader fileInput = new StreamReader(file.content, System.Text.Encoding.UTF8, true);

			try
			{
					do
					{
						#region uploading the file
						SubtitleFileChunk sfc = new SubtitleFileChunk();
						// sfc gets his ID when added to DB
						sfc.subtitleFileID = newFile.ID;

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
						if (!string.IsNullOrEmpty(line4))
						{
							sfc.subtitleLine2 = line4;
							var line5 = fileInput.ReadLine(); // "" 

							if (!string.IsNullOrEmpty(line5))
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

					} while (!fileInput.EndOfStream);
					int? ID = newFile.ID;
					fileInput.Dispose();
					return RedirectToAction("DisplayFile", new { id = ID });
						#endregion
				}
				catch (Exception)
				{
					subtitleFileRepo.DeleteSubtitleFileChunk(newFile.ID);
					subtitleFileRepo.DeleteSubtitleFile(newFile);
					return View("Error");
				}

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