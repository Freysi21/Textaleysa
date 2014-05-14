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
        #region Database and repo's
		private ApplicationDbContext db = new ApplicationDbContext();
        RequestRepository repo = new RequestRepository();
        VoteRepository vrepo = new VoteRepository();
        #endregion

        #region Yfirlit beiðna
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
                    request.ID = f.ID;
                    requests.Add(request);
                }
            return View(requests);   
        }
#endregion

        #region Velja þátt eða kvikmynd
        public ActionResult CreateRequest()
        {
            return View();
        }
        #endregion

        #region Búa til beiðni
        #region Beiðni fyrir mynd
        public ActionResult CreateMovieRequest()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult CreateMovieRequest(UploadMovieRequestViewModel request)
        {
            if(request == null)
            {
                return RedirectToAction("CreateRequest");
            }
                Request r = new Request();
                r.userName = User.Identity.Name;
                r.mediaTitle = (request.mediaTitle + " (" + (request.yearReleased.ToString()) + ")");
                r.date = DateTime.Now;
                r.mediaType = "Kvikmynd";
                r.language = request.language;
                repo.AddRequest(r);

                return RedirectToAction("RequestList");
                //return Json(r, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Beiðni fyrir þátt
        public ActionResult CreateEpisodeRequest()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult CreateEpisodeRequest(UploadEpisodeRequestViewModel request)
        {
            if (request == null)
            {
                return RedirectToAction("CreateRequest");
            }
            Request r = new Request();
            r.userName = User.Identity.Name;
            r.mediaTitle = (request.mediaTitle + "S" + (request.season) + "E" + (request.episode.ToString()));
            r.date = DateTime.Now;
            r.mediaType = "Þáttur";
            r.language = request.language;
            repo.AddRequest(r);

            return RedirectToAction("RequestList");
            //return Json(r, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion

        #region Eyða, i think...
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
        #endregion

        #region controllerar fyrir script fyrir UpVoteScript/VoteScript
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
            var votesForRequest = vrepo.GetVoteForRequest(vote); // get all the votes 

            var user = User.Identity.Name;
            vote.userName = "Jóhann";

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
        #endregion
    }
}