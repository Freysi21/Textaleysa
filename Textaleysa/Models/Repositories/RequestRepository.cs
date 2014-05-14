using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaleysa.DAL;
using System.Data.Entity;

namespace Textaleysa.Models.Repositories
{
    public class RequestRepository
    {
		HRContext db = new HRContext();

        #region GetRequests()
        public IEnumerable<Request> GetRequests()
        {
            var result = from r in db.requests
                         select r;
            return result;
        }
        #endregion

        #region GetRequestById
        public Request GetRequestById(int id)
		{
			var result = (from s in db.requests
						  where s.ID == id
						  select s).SingleOrDefault();
			return result;
		}
        #endregion

        #region AddRequest/Modify
        public void AddRequest(Request r) 
        {
			db.requests.Add(r);
			db.SaveChanges();
        }

		public void Modify(Request r)
		{
			db.Entry(r).State = EntityState.Modified;
			db.SaveChanges();
        }
        #endregion
    }
}
