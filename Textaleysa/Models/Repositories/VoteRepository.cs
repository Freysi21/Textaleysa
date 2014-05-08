using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.Repositories
{
    public class VoteRepository
    {
        private static VoteRepository instance;

        public static VoteRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new VoteRepository();
                return instance;
            }
        }
        private List<Vote> votes = null;
        private VoteRepository()
        {
            this.votes = new List<Vote>();
        }
        public IEnumerable<Vote> GetVotes()
        {
            var result = from v in votes
                         orderby v.requestID ascending
                         select v;
            return result;
        }

        public void AddVote(Vote v)
        {
            int newID = 1;
            if (votes.Count() > 0)
            {
                newID = votes.Max(x => x.ID) + 1;
            }
            v.ID = newID;
        }
    }
}