using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textaleysa.DAL;
using System.Data.Entity;

namespace Textaleysa.Models.Repositories
{
    public class VoteRepository
    {
		HRContext db = new HRContext();

        #region GetVotes
        public IEnumerable<Vote> GetVotes()
        {
            var result = from v in db.votes
                         orderby v.requestID ascending
                         select v;
            return result;
        }
        #endregion

        #region AddVote
        public void AddVote(Vote v)
        {
            db.votes.Add(v);
            db.SaveChanges();
        }
        #endregion

        #region GetVoteForRequest
        public IEnumerable<Vote> GetVoteForRequest(Vote vote)
        {
            var requests = from c in db.requests
                           select c; // get all comments
            var result = from r in requests
                         join v in db.votes on r.ID equals v.requestID // getting only likes that are linked to a particular comment
                         where v.requestID == vote.requestID
                         select v;

            return result;
        }
        #endregion
    }
}