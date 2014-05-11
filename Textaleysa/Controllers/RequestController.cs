using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models.Repositories;
using Textaleysa.DAL;
using Textaleysa.Models;



namespace Textaleysa.Controllers
{
    public class RequestController : Controller
    {
        private RequestContext db = new RequestContext();
        RequestRepository repo = new RequestRepository();
        //
        // GET: /Request/
        public ActionResult Request()
        {
            var model = repo.GetRequests();
            return View(model);
        }
        public ActionResult CreateRequest(Request request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Request r = new Request();
                r.userName = User.Identity.Name;
                r.mediaTitle = request.mediaTitle; // setting the CommentText we got from the input field
                r.date = DateTime.Now;
                r.mediaType = request.mediaType;
                r.language = request.language;
                repo.AddRequest(r); // add the comment to the db

                return Json(r, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult getRequests()
        {
            var requests = repo.GetRequests();
            var res = from r in requests
                      select new
                      {
                          date = r.date.ToString("dd. MMMM HH:mm"),
                          ID = r.ID,
                          mediaTitle = r.mediaTitle,
                          mediaType = r.mediaType,
                          Username = r.userName,
                          language = r.language,
                      };

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getVotes()
        {
            var votes = VoteRepository.Instance.GetVotes(); // get all the likes 

            // changes the format of LikeDate to a string to display it in a nice way 
            var result = from v in votes
                         select new
                         {
                             ID = v.ID,
                             requestID = v.requestID,
                             userName = v.userName
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult postVotes(Vote vote)
        {
            vote.requestID++; // some of by one error 
            var votesForRequest = VoteRepository.Instance.GetVoteForRequest(vote); // get all the votes 

            var user = User.Identity.Name;
            vote.userName = user;

            bool check = false;
            foreach (var v in votesForRequest) // go through the fixed list of votes
            {
                if (vote.userName == v.userName) // if the usernames match we don't add the vote to the db
                {
                    check = true;
                }
            }

            if (!check)
            {
                VoteRepository.Instance.AddVote(vote);
            }
            else
            {
                vote.userName = "";
            }
            return Json(vote, JsonRequestBehavior.AllowGet);
        }
	}
}