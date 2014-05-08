using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class RequestRepository
    {

        private static RequestRepository _instance;

        public static RequestRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RequestRepository();
                return _instance;
            }
        }

        private List<Request> m_requests = null;

        private RequestRepository()
        {
            this.m_requests = new List<Request>();
        }

        public IEnumerable<Request> GetRequests()
        {
            var result = from r in m_requests
                         orderby r.date ascending
                         select r;
            return result;
        }

        public void AddComment(Request r)
        {
            int newID = 1;
            if (m_requests.Count() > 0)
            {
                newID = m_requests.Max(x => x.ID) + 1;
            }
            r.ID = newID;
            r.date = DateTime.Now;
            m_requests.Add(r);
        }
    }
}
