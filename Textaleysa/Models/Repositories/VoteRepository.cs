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
        public IEnumerable<Vote> GetVoteForRequest(int id)
        {
            var result = from v in db.votes
                         where v.requestID == id
                         select v;

            return result;
        }
        #endregion
    }
}