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
		MovieContext movieDb = new MovieContext();

		public Movie GetMovieById(int id)
		{
			var result = (from m in movieDb.movies
						 where m.ID == id
						 select m).SingleOrDefault();
			return result;
		}

        public IEnumerable<Movie> GetMovieTitles()
        {
			var result = from m in movieDb.movies
						 orderby m.ID
                         select m;
            return result;
        }

        public void AddMediaTitle(Movie mt)
        {
			movieDb.movies.Add(mt);
			movieDb.SaveChanges();
        }

		public void Modify(Movie mt)
		{
			movieDb.Entry(mt).State = EntityState.Modified;
			movieDb.SaveChanges();
		}

    }
}