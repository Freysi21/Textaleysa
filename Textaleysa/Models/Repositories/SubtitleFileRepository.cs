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
		HRContext db = new HRContext();

        public IEnumerable<SubtitleFile> GetSubtitles()
        {
            var result = from f in db.subtitleFile
                         orderby f.ID ascending
                         select f;
            return result;
        }

        public void AddFile(SubtitleFile f)
        {
            int newID = 1;
			if (db.subtitleFile.Count() > 0)
            {
				newID = db.subtitleFile.Max(x => x.ID) + 1;
            }
            f.ID = newID;
        }

		public void AddSubtitleFile(SubtitleFile sf)
		{
			db.subtitleFile.Add(sf);
			db.SaveChanges();
		}

		public void Modify(SubtitleFile sf)
		{
			db.Entry(sf).State = EntityState.Modified;
			db.SaveChanges();
		}
    }
}
