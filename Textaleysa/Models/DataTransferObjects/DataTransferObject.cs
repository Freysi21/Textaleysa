using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaleysa.Models.Repositories;

namespace Textaleysa.Models.DataTransferOpjects
{
	public class SubtitleFileTransfer
	{
		SubtitleFileRepository repo = new SubtitleFileRepository();
	}

	public class MediaTitleTransfer
	{
		MediaTitleRepository repo = new MediaTitleRepository();

		public MediaTitle GetMovieById(int id)
		{
			var result = (from m in repo.GetAllMovieTitles()
						  where m.ID == id && m.isMovie == true
						  select m).SingleOrDefault();
			return result;
		}

		public MediaTitle GetMovieByTitle(string s)
		{
			var result = (from m in repo.GetAllMovieTitles()
						  where m.title == s && m.isMovie == true
						  select m).FirstOrDefault();
			return result;
		}

		public IEnumerable<MediaTitle> SearchAfterTitle(string s)
		{
			var result = from m in repo.GetAllMovieTitles()
						 where m.title.ToLower().Contains(s.ToLower()) && m.isMovie == true
						 select m;
			return result;
		}

		public MediaTitle GetSerieById(int id)
		{
			var result = (from m in repo.GetAllSerieTitles()
						  where m.ID == id && m.isMovie == false
						  select m).SingleOrDefault();
			return result;
		}

		public MediaTitle GetSerieByTitle(string s)
		{
			var result = (from m in repo.GetAllSerieTitles()
						  where m.title == s && m.isMovie == false
						  select m).FirstOrDefault();
			return result;
		}
	}
}