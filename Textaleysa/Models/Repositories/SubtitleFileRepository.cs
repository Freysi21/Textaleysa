using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;

namespace Textaleysa.Models.Repositories
{
    public class SubtitleFileRepository
    {
		SubtitleFileContext db = new SubtitleFileContext();
		SubtitleFileChunkContext chunkDb = new SubtitleFileChunkContext();

        public IEnumerable<SubtitleFile> GetSubtitles()
        {
            var result = from f in db.subtitleFile
                         orderby f.ID ascending
                         select f;
            return result;
        }

		public SubtitleFile GetSubtitleById(int? id)
		{
			var result = (from m in db.subtitleFile
						  where m.ID == id.Value
						  select m).FirstOrDefault();
			return result;
		}
		
		public IEnumerable<SubtitleFile> GetSubtitleFilesByMediaTitleId(int id)
		{
			var results = from f in db.subtitleFile
						  where f.mediaTitleID == id
						  select f;
			return results;
		}

		public void AddSubtitleFile(SubtitleFile sf)
		{
			sf.downloadCount = 0;
			sf.date = DateTime.Now;
			db.subtitleFile.Add(sf);
			db.SaveChanges();
		}

		public void ModifySubtitleFile(SubtitleFile sf)
		{
			db.Entry(sf).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void DeleteSubtitleFile(SubtitleFile sf)
		{
			db.subtitleFile.Remove(sf);
			db.SaveChanges();
		}
		// *************  SubtitleFileChunk starts here ***********

		public SubtitleFileChunk GetSubtitleFileChunkById(int id)
		{
			var result = (from s in chunkDb.subtitleFileChunk
						 where s.ID == id
						 select s).SingleOrDefault();
			return result;
		}

		public IEnumerable<SubtitleFileChunk> GetSubtitleFileChunks()
		{
			var result = from s in chunkDb.subtitleFileChunk
						 select s;
			return result;
		}

		public IEnumerable<SubtitleFileChunk> GetChunksBySubtitleFileID(int id)
		{
			var result = from c in chunkDb.subtitleFileChunk
								 where c.subtitleFileID == id
								 orderby c.lineID ascending
								 select c;
			return result;
		}

		public void AddSubtitleChunk(SubtitleFileChunk sfc)
		{
			chunkDb.subtitleFileChunk.Add(sfc);
			chunkDb.SaveChanges();
		}

		public void ModifySubtitleFileChunk(SubtitleFileChunk sfc)
		{
			chunkDb.Entry(sfc).State = EntityState.Modified;
			chunkDb.SaveChanges();
		}

		public void DeleteSubtitleFileChunk(int? id)
		{
			var chunks = from c in chunkDb.subtitleFileChunk
						 where c.subtitleFileID == id
						 select c;

			foreach (var item in chunks)
			{
				chunkDb.subtitleFileChunk.Remove(item);
			}
			chunkDb.SaveChanges();
		}
    }
}
