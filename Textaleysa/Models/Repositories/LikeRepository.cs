using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class LikeRepository
    {
        private static LikeRepository instance;

        public static LikeRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new LikeRepository();
                return instance;
            }
        }
        private List<Like> likes = null;
        private LikeRepository()
        {
            this.likes = new List<Like>();
			Like like1 = new Like { ID = 1, commentID = 1, userID = 1 };
			this.likes.Add(like1);
        }

        public IEnumerable<Like> GetLikes()
        {
            var result = from l in likes
                         orderby l.commentID ascending
                         select l;
            return result;
        }

        public void AddLike(Like l)
        {
            int newID = 1;
            if (likes.Count() > 0)
            {
                newID = likes.Max(x => x.ID) + 1;
            }
            l.ID = newID;
        }
    }
}