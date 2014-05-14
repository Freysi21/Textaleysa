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

		#region Move repo functions
		public Movie GetMovieById(int id)
		{
			var result = (from m in db.movies
						 where m.ID == id
						 select m).SingleOrDefault();
			return result;
		}
		
		public Movie GetMovieByTitle(string s)
		{
			var result = (from m in db.movies
						 where m.title == s
						 select m).FirstOrDefault();
			return result;
		}

        public IEnumerable<Movie> GetMovieTitles()
        {
			var result = from m in db.movies
						 orderby m.ID
                         select m;
            return result;
        }

		public IEnumerable<Movie> SearchAfterTitle(string s)
		{
			var result  = from m in db.movies
						 where m.title.ToLower().Contains(s.ToLower())
						 select m;
			return result;
		}

        public void AddMediaTitle(Movie mt)
        {
			db.movies.Add(mt);
			db.SaveChanges();
        }

		public void Modify(Movie mt)
		{
			db.Entry(mt).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(Movie mt)
		{
			db.movies.Remove(mt);
			db.SaveChanges();

		}
		#endregion

		#region Serie repo functions

		public Serie GetSerieById(int id)
		{
			var result = (from m in db.series
						  where m.ID == id
						  select m).SingleOrDefault();
			return result;
		}

		public Serie GetSerieByTitle(string s)
		{
			var result = (from m in db.series
						  where m.title == s
						  select m).FirstOrDefault();
			return result;
		}

		public IEnumerable<Serie> GetSerieTitles()
		{
			var result = from m in db.series
						 orderby m.ID
						 select m;
			return result;
		}

		public IEnumerable<Serie> SearchSerieAfterTitle(string s)
		{
			var result = from m in db.series
						 where m.title.ToLower().Contains(s.ToLower())
						 select m;
			return result;
		}

		public void AddMediaTitle(Serie s)
		{
			db.series.Add(s);
			db.SaveChanges();
		}

		public void Modify(Serie s)
		{
			db.Entry(s).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(Serie s)
		{
			db.series.Remove(s);
			db.SaveChanges();

		}
		#endregion
	}
}