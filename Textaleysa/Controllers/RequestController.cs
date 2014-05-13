using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models.Repositories;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.ViewModel;




namespace Textaleysa.Controllers
{
    public class RequestController : Controller
    {
        private RequestContext db = new RequestContext();
        RequestRepository repo = new RequestRepository();
        private VoteContext vdb = new VoteContext();
        VoteRepository vrepo = new VoteRepository();

        //
        // GET: /Request/
        //[HttpGet]
        public ActionResult RequestList()
        {

            List<ListRequestViewModel> requests = new List<ListRequestViewModel>();

            var files = from f in repo.GetRequests()
                        select f;
            if(files == null)
            {
                return View("Error");
            }
                foreach(var f in files)
                {
                    ListRequestViewModel request = new ListRequestViewModel();
                    request.userName = f.userName;
                    request.mediaTitle = f.mediaTitle;
                    request.language = f.language;
                    requests.Add(request);
                }
            return View(requests);
        }
        public ActionResult CreateRequest()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult CreateRequest(UploadMovieRequestViewModel request)
        {
            if(request == null)
            {
                return RedirectToAction("CreateRequest");
            }
            if (ModelState.IsValid)
            {
                Request r = new Request();
                r.userName = User.Identity.Name;
                r.mediaTitle = (request.mediaTitle + " (" + (request.yearReleased.ToString()) + ")");
                r.date = DateTime.Now;
                r.language = request.language;
                repo.AddRequest(r);

                return RedirectToAction("RequestList");
                //return Json(r, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(request);
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

        //--------------VOTE START HERE----------

        public ActionResult getVotes()
        {
            var votes = vrepo.GetVotes(); // get all the likes 

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
            vote.ID++;
            vote.requestID = vote.requestID + 12;
            var user = User.Identity.Name;
            vote.userName = user;
            var votesForRequest = vrepo.GetVoteForRequest(vote); // get all the votes 

            

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
                vrepo.AddVote(vote);
            }
            else
            {
                vote.userName = "";
            }
            return Json(vote, JsonRequestBehavior.AllowGet);
        }
	}
}