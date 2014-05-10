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

		public void AddSubtitleFile(SubtitleFile sf)
		{
			sf.downloadCount = 0;
			sf.date = DateTime.Now;
			db.subtitleFile.Add(sf);
			db.SaveChanges();
		}

		public void Modify(SubtitleFile sf)
		{
			db.Entry(sf).State = EntityState.Modified;
			db.SaveChanges();
		}

		// *************  SubtitleFileChunk starts here ***********

		public IEnumerable<SubtitleFileChunk> GetSubtitleFileChunks()
		{
			var result = from s in chunkDb.subtitleFileChunk
						 orderby s.ID ascending
						 select s;
			return result;
		}

		public void AddSubtitleChunk(SubtitleFileChunk sfc)
		{
			chunkDb.subtitleFileChunk.Add(sfc);
			chunkDb.SaveChanges();
		}

		public void Modify(SubtitleFileChunk sfc)
		{
			chunkDb.Entry(sfc).State = EntityState.Modified;
			chunkDb.SaveChanges();
		}
    }
}
