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
				#region Setting up ViewModel
				dmv.ID = subtitleFile.ID;
				dmv.title = movie.title;
				dmv.yearReleased = movie.yearReleased;
				dmv.grade = 10;
				dmv.userName = subtitleFile.userName;
				dmv.language = subtitleFile.language;
				dmv.date = subtitleFile.date;
				dmv.downloadCount = subtitleFile.downloadCount;
				#endregion
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

			var subtitleFileChunks = subtitleFileRepo.GetChunksBySubtitleFileID(subtitleFile.ID);

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

		public ActionResult UploadMovieFile()
		{
			UploadMovieModelView model = new UploadMovieModelView();
			model.languageOptions = langDb.GetLanguages();
			if (model == null)
			{
				return View("Error");
			}
			return View(model);
		}

		// TODO: Add authorized !
		[HttpPost]
		public ActionResult UploadMovieFile(UploadMovieModelView fileInfo, HttpPostedFileBase file)
		{
			if (file != null && fileInfo != null)
			{
				SubtitleFile subtitleFile = new SubtitleFile();
				var movie = meditaTitleRepo.GetMovieByTitle(fileInfo.title);
				// If the MediaTitle is not in the db we create a new title and connect the SubtitleFile
				// to the MediaTitle else we just connect.
				if (movie == null)
				{
					Movie m = new Movie();
					#region Adding to db and connecting
					m.title = fileInfo.title;
					m.yearReleased = fileInfo.yearReleased;
					meditaTitleRepo.AddMediaTitle(m);
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
					StreamReader fileInput = new StreamReader(file.InputStream, System.Text.Encoding.UTF8, true);
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
					return RedirectToAction("DisplayFile", new { id = ID });
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

		public ActionResult DeleteFile(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
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
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
			if (subtitleFile == null)
			{
				return View("Error");
			}

			var chunks = subtitleFileRepo.GetChunksBySubtitleFileID(subtitleFile.ID);
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

			EditFileView file = new EditFileView();
			file.content = result;
			file.ID = subtitleFile.ID;
			return View(file);
		}

		[HttpPost]
		public ActionResult EditFile(EditFileView file)
		{
			var subtitleFile = subtitleFileRepo.GetSubtitleById(file.ID);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

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
					return RedirectToAction("DisplayFile", newFile.ID);
						#endregion
				}
				catch (Exception)
				{
					subtitleFileRepo.DeleteSubtitleFileChunk(newFile.ID);
					subtitleFileRepo.DeleteSubtitleFile(newFile);
					return View("Error");
				}
		}

		public ActionResult DisplayContent(int? id)
		{
			#region if (id == null) return NOTFOUND
			if (id == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileRepo.GetSubtitleById(id.Value);
			#region if (subtitleFile == null) return NOTFOUND
			if (subtitleFile == null)
			{
				return View("Error");
			}
			#endregion

			var chunks = subtitleFileRepo.GetChunksBySubtitleFileID(subtitleFile.ID);
			#region if (chunks == null) return NOTFOUND
			if (chunks == null)
			{
				return View("Error");
			}
			#endregion

			var movieTitle = meditaTitleRepo.GetMovieById(subtitleFile.mediaTitleID);
			#region if (movieTitle == null) return NOTFOUND
			if (movieTitle == null)
			{
				return View("Error");
			}
			#endregion

			DisplayFileView file = new DisplayFileView();
			#region putting everything in place for the ViewModel
			file.title = movieTitle.title;
			file.yearReleased = movieTitle.yearReleased;
			file.language = subtitleFile.language;
			file.content = new List<DisplayContentFileView>();
			foreach(var item in chunks)
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
		
		public ActionResult EditChunk(DisplayContentFileView chunkInput)
		{
			var chunk = subtitleFileRepo.GetSubtitleFileChunkById(chunkInput.ID);
			#region if (chunk == null) return NOTFOUND
			if (chunk == null)
			{
				return View("Error");
			}
			#endregion

			var subtitleFile = subtitleFileRepo.GetSubtitleById(chunk.subtitleFileID);
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
			return RedirectToAction("DisplayContent", subtitleFile.ID);

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