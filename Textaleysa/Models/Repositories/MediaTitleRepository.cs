using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;

namespace Textaleysa.Models.Repositories
{
    public class MediaTitleRepository
    {
		HRContext db = new HRContext();
		
		public IEnumerable<MediaTitle> GetAllMovieTitles()
        {
			var result = from m in db.mediaTitles
						 where m.isMovie == true
						 orderby m.ID
                         select m;
            return result;
        }

		public IEnumerable<MediaTitle> GetAllSerieTitles()
		{
			var result = from m in db.mediaTitles
						 where m.isMovie == false
						 orderby m.ID 
						 select m;
			return result;
		}

		public void AddMediaTitle(MediaTitle s)
		{
			db.mediaTitles.Add(s);
			db.SaveChanges();
		}

		public void Modify(MediaTitle s)
		{
			db.Entry(s).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(MediaTitle s)
		{
			db.mediaTitles.Remove(s);
			db.SaveChanges();

		}
	}
}