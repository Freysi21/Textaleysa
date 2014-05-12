using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaleysa.DAL;
using System.Data.Entity;

namespace Textaleysa.Models.Repositories
{
    public class CommentRepository
    {
		CommentContext db = new CommentContext();

        public IEnumerable<Comment> GetComments()
        {
            var result = from c in db.comments
                         select c;
            return result;
        }

		public Comment GetCommentById(int id)
		{
			var result = (from s in db.comments
						  where s.ID == id
						  select s).SingleOrDefault();
			return result;
		}

        public void AddComment(Comment c) 
        {
			db.comments.Add(c);
			db.SaveChanges();
        }

		public void Modify(Comment c)
		{
			db.Entry(c).State = EntityState.Modified;
			db.SaveChanges();
		}
    }
}

