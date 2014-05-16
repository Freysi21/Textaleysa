using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class GradeRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        #region Get
        public IEnumerable<Grade> GetGrades()
        {
            var result = from v in db.grades
                         orderby v.fileID ascending
                         select v;
            return result;
        }
        #endregion

        #region Add
        public void AddGrade(Grade g)
        {
            db.grades.Add(g);
            db.SaveChanges();
        }
        #endregion

        #region GetGradeForFile
        public IEnumerable<Grade> GetGradeForFile(int id)
        {
            var result = from v in db.grades
                         where v.fileID == id
                         select v;

            return result;
        }
        #endregion
        public double GetAvgForFile(int id)
        {
            var result = from v in db.grades
                         where v.fileID == id
                         select v;
            double sum = 0;
            foreach(var g in result)
            {
                sum = g.mediaGrade + sum;
            }
            if (result.Count() != 0)
            {
                sum = (sum / result.Count());
            }
            else
            {
                sum = 0;
            }

            return sum;
        }
    }
}