using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;
using Textaleysa.Repositories;

namespace Textaleysa.Models.Repositories
{
    public class SubtitleFileRepository : ISubtitleRepository
    {
        private HRContext _context;
		HRContext db = new HRContext();


        public IQueryable<SubtitleFile> GetAllSubtitles()
        {
            var result = from f in db.subtitleFile
                         orderby f.ID ascending
                         select f;
            return result;
        }


		#region SubtitleFile functions

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
		#endregion
		
		#region SubtitleFileChunk functions

		public IEnumerable<SubtitleFileChunk> GetAllSubtitleFileChunks()
		{
			var result = from s in db.subtitleFileChunk
						 select s;
			return result;
		}

		public void AddSubtitleChunk(SubtitleFileChunk sfc)
		{
			db.subtitleFileChunk.Add(sfc);
			db.SaveChanges();
		}

		public void ModifySubtitleFileChunk(SubtitleFileChunk sfc)
		{
			db.Entry(sfc).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void DeleteSubtitleFileChunk(int? id)
		{
			var chunks = from c in db.subtitleFileChunk
						 where c.subtitleFileID == id
						 select c;

			foreach (var item in chunks)
			{
				db.subtitleFileChunk.Remove(item);
			}
			db.SaveChanges();
		}
		#endregion
	}
}
