﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaleysa.Models.Repositories;

namespace Textaleysa.Models.DataTransferOpjects
{
	public class SubtitleFileTransfer
	{
		SubtitleFileRepository repo = new SubtitleFileRepository();

		public SubtitleFile GetSubtitleById(int? id)
		{
			var result = (from m in repo.GetAllSubtitles()
						  where m.ID == id.Value
						  select m).FirstOrDefault();
			return result;
		}

		public IEnumerable<SubtitleFile> GetSubtitleFilesByMediaTitleId(int id)
		{
			var results = from f in repo.GetAllSubtitles()
						  where f.mediaTitleID == id
						  select f;
			return results;
		}

		public SubtitleFileChunk GetSubtitleFileChunkById(int id)
		{
			var result = (from s in repo.GetAllSubtitleFileChunks()
						  where s.ID == id
						  select s).SingleOrDefault();
			return result;
		}

		public IEnumerable<SubtitleFileChunk> GetChunksBySubtitleFileID(int id)
		{
			var result = from c in repo.GetAllSubtitleFileChunks()
						 where c.subtitleFileID == id
						 orderby c.lineID ascending
						 select c;
			return result;
		}

		public SubtitleFileChunk GetCunkByID(int id)
		{
			var result = (from c in repo.GetAllSubtitleFileChunks()
						 where c.ID == id
						 select c).SingleOrDefault();
			return result;
		}
	}

	public class MediaTitleTransfer
	{
		MediaTitleRepository repo = new MediaTitleRepository();

		public MediaTitle GetMediaTitleById(int id)
		{
			var result = (from m in repo.GetAllMediaTitles()
						 where m.ID == id
						 select m).SingleOrDefault();
			return result;
		}

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

		public MediaTitle GetMovieByTitleAndYear(string s, int year)
		{
			var result = (from m in repo.GetAllMovieTitles()
						  where m.title == s && m.yearReleased == year
						  select m).FirstOrDefault();
			return result;
		}

		public IEnumerable<MediaTitle> SearchAfterTitle(string s)
		{
			var result = from m in repo.GetAllMediaTitles()
						 where m.title.ToLower().Contains(s.ToLower())
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

		public MediaTitle GetSerieByTitleAndInfo(string title, int season, int episode)
		{
			var result = (from s in repo.GetAllSerieTitles()
						  where s.title == title && s.season == season && s.episode == episode
						  select s).SingleOrDefault();
			return result;
		}
	}
}