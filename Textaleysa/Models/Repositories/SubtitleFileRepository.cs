using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;
using Textaleysa.Repos;

namespace Textaleysa.Models.Repositories
{
    public class SubtitleFileRepository : ISubtitleFileRepository
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

		public void ModifySubtitleFileChunk(SubtitleFileChunk sfc)
		{
			chunkDb.Entry(sfc).State = EntityState.Modified;
			chunkDb.SaveChanges();
		}


        public IQueryable<SubtitleFile> GetSubtitlefiles()
        {
            throw new NotImplementedException();
        }
    }
}
