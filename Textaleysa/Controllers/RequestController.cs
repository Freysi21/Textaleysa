using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models.Repositories;

namespace Textaleysa.Controllers
{
    public class RequestController : Controller
    {
        //
        // GET: /Request/
        public ActionResult Request()
        {
            var model = RequestRepository.Instance.GetRequests();
            return View(model);
        }
        public ActionResult CreateRequest(Request request)
        {
            Request r = new Request();
            /*
            var user = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // get user name from computer username

            // cuts of the 'WINDOWS\\' part of the username
            int slashPos = user.IndexOf("\\");
            if (slashPos != -1)
            {
                user = user.Substring(slashPos + 1);
            } */
            var user = User.Identity.Name;
            r.userName = user;

            r.mediaTitle = request.mediaTitle; // setting the CommentText we got from the input field
            r.date = DateTime.Now;
            r.mediaType = request.mediaType;
            r.language = request.language;
            RequestRepository.Instance.AddRequest(r); // add the comment to the db

            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getRequests()
        {
            var requests = RequestRepository.Instance.GetRequests(); // get all the comments

            // changes the format of CommentDate to a string to display it in a nice way 
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
                             date = v.date.ToString("dd. MMMM HH:mm"),
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