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
        private IContext _data { get; set; }
		HRContext db = new HRContext();
<<<<<<< HEAD
        public SubtitleFileRepository(IContext dataContext = null)
        {
            _data = dataContext ?? new HRContext();
        }
        public IEnumerable<SubtitleFile> GetSubtitles()
=======

		#region SubtitleFile functions
		public IEnumerable<SubtitleFile> GetAllSubtitles()
>>>>>>> eb3a66d990f628cd4a0f55156358eed76d32e5ed
        {
            var result = from f in _data.subtitleFile
                         orderby f.ID ascending
                         select f;
            return result;
        }
        /*
        public IEnumerable<SubtitleFile> GetSubtitles()
        {
            var result = from f in db.subtitleFile
                         orderby f.ID ascending
                         select f;
            return result;
        }*/

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
