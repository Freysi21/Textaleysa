using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class GradeRepository
    {
private static GradeRepository instance;

        public static GradeRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new GradeRepository();
                return instance;
            }
        }
        private List<Grade> grades = null;
        private GradeRepository()
        {
            this.grades = new List<Grade>();
        }
        public IEnumerable<Grade> GetGrades()
        {
            var result = from v in grades
                         orderby v.fileID ascending
                         select v;
            return result;
        }

        public void AddGrade(Grade g)
        {
            int newID = 1;
            if (grades.Count() > 0)
            {
                newID = grades.Max(x => x.ID) + 1;
            }
            g.ID = newID;
        }
    }
}