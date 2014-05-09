using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class MediaTitleRepository
    {
    private static MediaTitleRepository instance;

        public static MediaTitleRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MediaTitleRepository();
                return instance;
            }
        }

        private List<MediaTitle> mediaTitles = null;

        private MediaTitleRepository()
        {
            this.mediaTitles = new List<MediaTitle>();
        }

        public IEnumerable<MediaTitle> GetMediaTitles()
        {
            var result = from t in mediaTitles
                         orderby t.title ascending
                         select t;
            return result;
        }

        public void AddMediaTitle(MediaTitle t)
        {
            var result = from m in mediaTitles
                         where m.title == t.title
                         select m;
            if (result == null)
            {
                int newID = 1;
                if (mediaTitles.Count() > 0)
                {
                    newID = mediaTitles.Max(x => x.ID) + 1;
                }
                t.ID = newID;
            }
        }
    }
}