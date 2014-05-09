using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;


namespace Textaleysa.Models.Repositories
{
    public class CommentRepository
    {
<<<<<<< HEAD
        private static CommentRepository _instance;

        public static CommentRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommentRepository();
                return _instance;
            }
        }

        private List<Comment> m_comments = null;

        private CommentRepository()
        {
			this.m_comments = new List<Comment>();
        }

        public IEnumerable<Comment> GetComments()
        {
            var result = from c in m_comments
                         orderby c.date ascending
=======
		HRContext db = new HRContext();

        public IEnumerable<Comment> GetComments()
        {
            var result = from c in db.comments
>>>>>>> 77cbce21d2fa77e9ccc2ba6da1dec9bf0b889352
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
<<<<<<< HEAD
            int newID = 1;
            if (m_comments.Count() > 0)
            {
                newID = m_comments.Max(x => x.ID) + 1;
            }
            c.ID = newID;
            c.date = DateTime.Now;
            m_comments.Add(c);
=======
			db.comments.Add(c);
			db.SaveChanges();
>>>>>>> 77cbce21d2fa77e9ccc2ba6da1dec9bf0b889352
        }

		public void Modify(Comment c)
		{
			db.Entry(c).State = EntityState.Modified;
			db.SaveChanges();
		}
    }
}

