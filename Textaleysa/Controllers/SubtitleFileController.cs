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
using Textaleysa.Models.DataTransferOpjects;
using Textaleysa.Models.Repositories;
using Textaleysa.Models.ViewModel;

namespace Textaleysa.Controllers
{
	public class SubtitleFileController : Controller
	{
		ApplicationDbContext db = new ApplicationDbContext();
		SubtitleFileRepository subtitleFileRepo = new SubtitleFileRepository();
		MediaTitleRepository mediaTitleRepo = new MediaTitleRepository();
		SubtitleFileTransfer subtitleFileTransfer = new SubtitleFileTransfer();
		MediaTitleTransfer mediaTitleTransfer = new MediaTitleTransfer();
        GradeRepository grepo = new GradeRepository();


		private LanguageRepository langDb = new LanguageRepository();

		// GET: /SubtitleFile/
		[HttpGet]
		public ActionResult Index()
		{

			return View();
		}

		public ActionResult DisplayMovie(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}

			var movie = mediaTitleTransfer.GetMovieById(subtitleFile.mediaTitleID);
			if (movie == null)
			{
				return View("Error");
			}
			else
			{
				DisplayMovieView dmv = new DisplayMovieView();
				#region Setting up ViewModel
				dmv.ID = subtitleFile.ID;
				dmv.title = movie.title;
				dmv.yearReleased = movie.yearReleased;
                dmv.grade = grepo.GetAvgForFile(id.Value);
				dmv.userName = subtitleFile.userName;
				dmv.language = subtitleFile.language;
				dmv.date = subtitleFile.date;
				dmv.downloadCount = subtitleFile.downloadCount;
				#endregion
				return View(dmv);
			}
		}

		public ActionResult DisplaySerie(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}

			var serie = mediaTitleTransfer.GetSerieById(subtitleFile.mediaTitleID);
			if (serie == null)
			{
				return View("Error");
			}
			else
			{
				DisplaySerieView model = new DisplaySerieView();
				#region Setting up ViewModel
				model.ID = subtitleFile.ID;
				model.date = subtitleFile.date;
				model.language = subtitleFile.language;
				model.downloadCount = subtitleFile.downloadCount;
				model.userName = subtitleFile.userName;
				model.title = serie.title;
				model.season = serie.season;
				model.episode = serie.episode;
                model.grade = grepo.GetAvgForFile(id.Value);
				#endregion
				return View(model);
			}
		}

		public FileStreamResult DownloadFile(int? id)
		{
			if (id == null)
			{
				return null;
			}

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return null;
			}
			var movie = mediaTitleTransfer.GetMovieById(subtitleFile.mediaTitleID);
			if (movie == null)
			{
				return null;
			}

			var subtitleFileChunks = subtitleFileTransfer.GetChunksBySubtitleFileID(subtitleFile.ID);

			var result = "";
			#region result = subtittleFileChunks
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
			#endregion

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

		[Authorize]
		public ActionResult Upload()
		{
			return View();
		}

		[Authorize]
		public ActionResult UploadMovie()
		{
			UploadMovieModelView model = new UploadMovieModelView();
			model.languageOptions = langDb.GetLanguages();
			if (model == null)
			{
				return View("Error");
			}
			return View(model);
		}

		[Authorize]
		public ActionResult UploadSerie()
		{
			UploadSerieModelView model = new UploadSerieModelView();
			model.languageOptions = langDb.GetLanguages();
			if (model == null)
			{
				return View("Error");
			}
			return View(model);
		}

		// TODO: Add authorized !
		[HttpPost]
		public ActionResult UploadMovie(UploadMovieModelView fileInfo, HttpPostedFileBase file)
		{
			if (file != null && fileInfo != null)
			{
				SubtitleFile subtitleFile = new SubtitleFile();
				var movie = mediaTitleTransfer.GetMovieByTitleAndYear(fileInfo.title, fileInfo.yearReleased);
				// If the MediaTitle is not in the db we create a new title and connect the SubtitleFile
				// to the MediaTitle else we just connect.
				if (movie == null)
				{
					MediaTitle m = new MediaTitle();
					#region Adding to db and connecting
					m.title = fileInfo.title;
					m.yearReleased = fileInfo.yearReleased;
					m.isMovie = true;
					mediaTitleRepo.AddMediaTitle(m);
					subtitleFile.mediaTitleID = m.ID;
					#endregion
				}
				else
				{
					subtitleFile.mediaTitleID = movie.ID;
				}

				var lan = langDb.GetLanguageById(fileInfo.languageID);
				subtitleFile.language = lan.language;
				subtitleFile.userName = User.Identity.Name;
				// Add the subtitleFile in DB
				subtitleFileRepo.AddSubtitleFile(subtitleFile);

				try
				{
					// Read the whole input file
					StreamReader fileInput = new StreamReader(file.InputStream, System.Text.Encoding.Default, true);
					do
					{

						SubtitleFileChunk sfc = new SubtitleFileChunk();
						#region putting everything in place
						sfc.subtitleFileID = subtitleFile.ID;

						// Read the first line of SubtitleFileChunk which is the ID of SubtitleFileChunk
						var line = fileInput.ReadLine();
						sfc.lineID = Convert.ToInt32(line);

						// Split the Subtitle time in to 3 parts (ex. line2[0] = "00:00:55,573" 
						// line2[1] = "-->" line2[2] = "00:00:58,867")
						var line2 = fileInput.ReadLine().Split(' ');
						sfc.startTime = line2[0];
						sfc.stopTime = line2[2];

						// Read the first line of text in the subtitlechunk
						var line3 = fileInput.ReadLine();
						sfc.subtitleLine1 = line3;

						// Read the second and third line of text but if the line is empty
						// we add the SubtitleFileChunk to the db, and loop
						var line4 = fileInput.ReadLine();
						if (!string.IsNullOrEmpty(line4))
						{
							sfc.subtitleLine2 = line4;
							var line5 = fileInput.ReadLine();

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
						#endregion
						subtitleFileRepo.AddSubtitleChunk(sfc);

					} while (!fileInput.EndOfStream);

					int? ID = subtitleFile.ID;
					return RedirectToAction("DisplayMovie", new { id = ID });
				}
				catch (Exception)
				{
					subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
					subtitleFileRepo.DeleteSubtitleFile(subtitleFile);
					return View("Error");
				}
			}
			return RedirectToAction("UploadMovie");
		}

		[HttpPost]
		public ActionResult UploadSerie(UploadSerieModelView fileInfo, HttpPostedFileBase file)
		{
			if (file != null && fileInfo != null)
			{
				SubtitleFile subtitleFile = new SubtitleFile();
				var serie = mediaTitleTransfer.GetSerieByTitleAndInfo(
					fileInfo.title, fileInfo.season, fileInfo.episode);
				// If the MediaTitle is not in the db we create a new title and connect the SubtitleFile
				// to the MediaTitle else we just connect.
				if (serie == null)
				{
					MediaTitle s = new MediaTitle();
					#region Adding to db and connecting
					s.title = fileInfo.title;
					s.season = fileInfo.season;
					s.episode = fileInfo.episode;
					s.isMovie = false;
					mediaTitleRepo.AddMediaTitle(s);
					subtitleFile.mediaTitleID = s.ID;
					#endregion
				}
				else
				{
					subtitleFile.mediaTitleID = serie.ID;
				}

				var lan = langDb.GetLanguageById(fileInfo.languageID);
				subtitleFile.language = lan.language;
				subtitleFile.userName = User.Identity.Name;
				// Add the subtitleFile in DB
				subtitleFileRepo.AddSubtitleFile(subtitleFile);

				try
				{
					// Read the whole input file
					StreamReader fileInput = new StreamReader(file.InputStream, System.Text.Encoding.Default, true);
					do
					{

						SubtitleFileChunk sfc = new SubtitleFileChunk();
						#region putting everything in place
						sfc.subtitleFileID = subtitleFile.ID;

						// Read the first line of SubtitleFileChunk which is the ID of SubtitleFileChunk
						var line = fileInput.ReadLine();
						sfc.lineID = Convert.ToInt32(line);

						// Split the Subtitle time in to 3 parts (ex. line2[0] = "00:00:55,573" 
						// line2[1] = "-->" line2[2] = "00:00:58,867")
						var line2 = fileInput.ReadLine().Split(' ');
						sfc.startTime = line2[0];
						sfc.stopTime = line2[2];

						// Read the first line of text in the subtitlechunk
						var line3 = fileInput.ReadLine();
						sfc.subtitleLine1 = line3;

						// Read the second and third line of text but if the line is empty
						// we add the SubtitleFileChunk to the db, and loop
						var line4 = fileInput.ReadLine();
						if (!string.IsNullOrEmpty(line4))
						{
							sfc.subtitleLine2 = line4;
							var line5 = fileInput.ReadLine();

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
						#endregion
						subtitleFileRepo.AddSubtitleChunk(sfc);

					} while (!fileInput.EndOfStream);

					int? ID = subtitleFile.ID;
					return RedirectToAction("DisplaySerie", new { id = ID });
				}
				catch (Exception)
				{
					subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
					subtitleFileRepo.DeleteSubtitleFile(subtitleFile);
					return View("Error");
				}
			}
			return RedirectToAction("UploadMovie");
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult DeleteFile(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
			subtitleFileRepo.DeleteSubtitleFile(subtitleFile);

			return View("Index");
		}

		[Authorize(Roles = "Administrators")]
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
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult EditMovie(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}
			var movie = mediaTitleTransfer.GetMovieById(subtitleFile.mediaTitleID);
			if (movie == null)
			{
				return View("Error");
			}

			var chunks = subtitleFileTransfer.GetChunksBySubtitleFileID(subtitleFile.ID);
			string result = "";

			foreach (var item in chunks)
			{
				#region converting subtitleFileChunks into string
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
				#endregion
			}

			EditMovieView file = new EditMovieView();
			file.title = movie.title;
			file.yearReleased = movie.yearReleased;
			file.language = subtitleFile.language;
			file.ID = subtitleFile.ID;
			file.content = result;
			return View(file);
		}

		[Authorize]
		public ActionResult EditSerie(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}
			var serie = mediaTitleTransfer.GetSerieById(subtitleFile.mediaTitleID);
			if (serie == null)
			{
				return View("Error");
			}

			var chunks = subtitleFileTransfer.GetChunksBySubtitleFileID(subtitleFile.ID);
			string result = "";

			foreach (var item in chunks)
			{
				#region converting subtitleFileChunks into string
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
				#endregion
			}

			EditSerieView file = new EditSerieView();
			file.title = serie.title;
			file.season = serie.season;
			file.episode = serie.episode;
			file.language = subtitleFile.language;
			file.ID = subtitleFile.ID;
			file.content = result;
			return View(file);
		}

		[HttpPost]
		public ActionResult EditFile(EditFileView file)
		{
			var subtitleFile = subtitleFileTransfer.GetSubtitleById(file.ID);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			var mediaTitle = mediaTitleTransfer.GetMediaTitleById(subtitleFile.ID);

			SubtitleFile newFile = new SubtitleFile();
			#region newfile = subtitlFile
			newFile.language = subtitleFile.language;
			newFile.mediaTitleID = subtitleFile.mediaTitleID;
			newFile.userName = subtitleFile.userName;
			newFile.downloadCount = subtitleFile.downloadCount;
			newFile.date = subtitleFile.date;
			#endregion
			subtitleFileRepo.AddSubtitleFile(newFile);

			MemoryStream tempContent = new MemoryStream(Encoding.UTF8.GetBytes(file.content));
			StreamReader fileInput = new StreamReader(tempContent, System.Text.Encoding.UTF8, true);
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
				subtitleFileRepo.DeleteSubtitleFileChunk(subtitleFile.ID);
				subtitleFileRepo.DeleteSubtitleFile(subtitleFile);
				if (mediaTitle.isMovie)
				{
					return RedirectToAction("DisplayMovie", newFile.ID);
				}
				else
				{
					return RedirectToAction("DisplaySerie", newFile.ID);
				}
					#endregion
			}
			catch (Exception)
			{
				subtitleFileRepo.DeleteSubtitleFileChunk(newFile.ID);
				subtitleFileRepo.DeleteSubtitleFile(newFile);
				return View("Error");
			}
		}

		public ActionResult DisplayMovieContent(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			var chunks = subtitleFileTransfer.GetChunksBySubtitleFileID(subtitleFile.ID);
			#region if (chunks == null) return NOTFOUND
			if (chunks == null)
			{
				return View("Error");
			}
			#endregion

			var movieTitle = mediaTitleTransfer.GetMovieById(subtitleFile.mediaTitleID);
			#region if (movieTitle == null) return NOTFOUND
			if (movieTitle == null)
			{
				return View("Error");
			}
			#endregion

			DisplayMovieFileView file = new DisplayMovieFileView();
			#region putting everything in place for the ViewModel
			file.title = movieTitle.title;
			file.yearReleased = movieTitle.yearReleased;
			file.language = subtitleFile.language;
			file.content = new List<DisplayContentFileView>();
			foreach (var item in chunks)
			{
				DisplayContentFileView content = new DisplayContentFileView();
				#region putting everything into place for the ViewModel list
				content.ID = item.ID;
				content.lineID = item.lineID;
				content.startTime = item.startTime;
				content.stopTime = item.stopTime;
				content.line1 = item.subtitleLine1;
				content.line2 = item.subtitleLine2;
				content.line3 = item.subtitleLine3;
				#endregion
				file.content.Add(content);
			}
			#endregion
			return View(file);
		}

		public ActionResult DisplaySerieContent(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(id.Value);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			var chunks = subtitleFileTransfer.GetChunksBySubtitleFileID(subtitleFile.ID);
			#region if (chunks == null) return NOTFOUND
			if (chunks == null)
			{
				return View("Error");
			}
			#endregion

			var serieTitle = mediaTitleTransfer.GetSerieById(subtitleFile.mediaTitleID);
			#region if (serieTitle == null) return NOTFOUND
			if (serieTitle == null)
			{
				return View("Error");
			}
			#endregion

			DisplaySerieFileView file = new DisplaySerieFileView();
			#region putting everything in place for the ViewModel
			file.title = serieTitle.title;
			file.season = serieTitle.season;
			file.episode = serieTitle.episode;
			file.language = subtitleFile.language;
			file.content = new List<DisplayContentFileView>();
			foreach (var item in chunks)
			{
				DisplayContentFileView content = new DisplayContentFileView();
				#region putting everything into place for the ViewModel list
				content.ID = item.ID;
				content.lineID = item.lineID;
				content.startTime = item.startTime;
				content.stopTime = item.stopTime;
				content.line1 = item.subtitleLine1;
				content.line2 = item.subtitleLine2;
				content.line3 = item.subtitleLine3;
				#endregion
				file.content.Add(content);
			}
			#endregion
			return View(file);
		}

		[Authorize]
		[HttpGet]
		public ActionResult EditChunk(int? id)
		{
			if (id == null)
			{
				return View("Error");
			}
			var chunk = subtitleFileTransfer.GetCunkByID(id.Value);
			if (chunk == null)
			{
				return View("Error");
			}

			DisplayContentFileView subtitleChunk = new DisplayContentFileView();
			subtitleChunk.ID = chunk.ID;
			subtitleChunk.lineID = chunk.lineID;
			subtitleChunk.startTime = chunk.startTime;
			subtitleChunk.stopTime = chunk.stopTime;
			subtitleChunk.line1 = chunk.subtitleLine1;
			subtitleChunk.line2 = chunk.subtitleLine2;
			subtitleChunk.line3 = chunk.subtitleLine3;
			return View(subtitleChunk);
		}

		[Authorize]
		[HttpPost]
		public ActionResult EditChunk(DisplayContentFileView chunkInput)
		{
			var chunk = subtitleFileTransfer.GetSubtitleFileChunkById(chunkInput.ID);
			#region if (chunk == null) return NOTFOUND
			if (chunk == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileTransfer.GetSubtitleById(chunk.subtitleFileID);
			#region if (subtitlefile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			#region chunk = chunkInput
			chunk.startTime = chunkInput.startTime;
			chunk.stopTime = chunkInput.stopTime;
			chunk.subtitleLine1 = chunkInput.line1;
			chunk.subtitleLine2 = chunkInput.line2;
			chunk.subtitleLine3 = chunkInput.line3;
			#endregion

			subtitleFileRepo.ModifySubtitleFileChunk(chunk);

			var mediaTitle = mediaTitleTransfer.GetMediaTitleById(subtitleFile.mediaTitleID);
			if (mediaTitle == null)
			{ 
				return View("Error");
			}
			if (mediaTitle.isMovie)
			{
				return RedirectToAction("DisplayMovieContent", subtitleFile.ID);
			}
			else
			{
				return RedirectToAction("DisplaySerieContent", subtitleFile.ID);
			}

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

        #region controllerar fyrir Script


        public ActionResult getGrades(Grade grade)
        {
            var avg = new { avrage = grepo.GetAvgForFile(grade.fileID), ID = grade.fileID };
            return Json(avg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult postGrade(Grade grade)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (grade.mediaGrade > 10 || grade.mediaGrade < 0)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var gradeForFile = grepo.GetGradeForFile(grade.fileID); // get all the votes 
            var user = User.Identity.Name;
            grade.userName = user;

            bool check = false;
            if (gradeForFile.Count() != 0)
            {
                foreach (var g in gradeForFile) // go through the fixed list of votes
                {
                    if (grade.userName == g.userName) // if the usernames match we don't add the vote to the db
                    {
                        check = true;
                    }
                }
            }

            if (!check)
            {
                grepo.AddGrade(grade);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            /*string dateTime = request.date.ToString("dd. MMMM HH:mm");
            var result = new  {
                               ID = request.ID.ToString(),
                               userName = request.userName,
                               mediaType = request.mediaType,
                               mediaTitle = request.mediaTitle,
                               language = request.language, 
                               date = dateTime
                               };*/

            return Json(grade, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}