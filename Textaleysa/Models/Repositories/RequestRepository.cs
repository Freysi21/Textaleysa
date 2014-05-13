using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class RequestRepository
    {

        private static RequestRepository instance;

        public static RequestRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new RequestRepository();
                return instance;
            }
        }

        private List<Request> requests = null;

        private RequestRepository()
        {
            this.requests = new List<Request>();
        }

        public IEnumerable<Request> GetRequests()
        {
            var result = from r in requests
                         orderby r.date ascending
                         select r;
            return result;
        }

        public void AddComment(Request r)
        {
            int newID = 1;
            if (requests.Count() > 0)
            {
                newID = requests.Max(x => x.ID) + 1;
            }
            r.ID = newID;
            r.date = DateTime.Now;
            requests.Add(r);
        }
    }
}
