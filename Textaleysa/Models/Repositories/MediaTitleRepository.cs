﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;

namespace Textaleysa.Models.Repositories
{
    public class MediaTitleRepository
    {
		ApplicationDbContext db = new ApplicationDbContext();
		
		public IEnumerable<MediaTitle> GetAllMovieTitles()
        {
			var result = from m in db.meditaTitles
						 where m.isMovie == true
						 orderby m.ID
                         select m;
            return result;
        }

		public IEnumerable<MediaTitle> GetAllSerieTitles()
		{
			var result = from m in db.meditaTitles
						 where m.isMovie == false
						 orderby m.ID 
						 select m;
			return result;
		}

		public IEnumerable<MediaTitle> GetAllMediaTitles()
		{
			var result = from m in db.meditaTitles
						 select m;
			return result;
		}

		public void AddMediaTitle(MediaTitle s)
		{
			db.meditaTitles.Add(s);
			db.SaveChanges();
		}

		public void Modify(MediaTitle s)
		{
			db.Entry(s).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(MediaTitle s)
		{
			db.meditaTitles.Remove(s);
			db.SaveChanges();

		}
	}
}