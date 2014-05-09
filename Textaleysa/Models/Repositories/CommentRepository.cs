using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class CommentRepository
    {
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
                         select c;
            return result;
        }

        public void AddComment(Comment c)
        {
            int newID = 1;
            if (m_comments.Count() > 0)
            {
                newID = m_comments.Max(x => x.ID) + 1;
            }
            c.ID = newID;
            c.date = DateTime.Now;
            m_comments.Add(c);
        }
    }
}

