using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Textaleysa.Models.Repositories
{
    public class CommentRepository
    {
        private static CommentRepository instance;

        public static CommentRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CommentRepository();
                return instance;
            }
        }

        private List<Comment> comments = null;

        private CommentRepository()
        {
			this.comments = new List<Comment>();
        }

        public IEnumerable<Comment> GetComments() // blafjfjjfjf
        {
            var result = from c in comments
                         orderby c.date ascending
                         select c;
            return result;
        }

        public void AddComment(Comment c) // hae
        {
            int newID = 1;
            if (comments.Count() > 0)
            {
                newID = comments.Max(x => x.ID) + 1;
            }
            c.ID = newID;
            c.date = DateTime.Now;
            comments.Add(c);
        }
    }
}

