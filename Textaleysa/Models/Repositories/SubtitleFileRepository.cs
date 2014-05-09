using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class SubtitleFileRepository
    {
        private static SubtitleFileRepository instance;

        public static SubtitleFileRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new SubtitleFileRepository();
                return instance;
            }
        }
        private List<SubtitleFile> files = null;
        private SubtitleFileRepository()
        {
            this.files = new List<SubtitleFile>();
        }
        public IEnumerable<SubtitleFile> GetSubtitles()
        {
            var result = from f in files
                         orderby f.ID ascending
                         select f;
            return result;
        }

        public void AddFile(SubtitleFile f)
        {
            int newID = 1;
            if (files.Count() > 0)
            {
                newID = files.Max(x => x.ID) + 1;
            }
            f.ID = newID;
        }
    }
}
