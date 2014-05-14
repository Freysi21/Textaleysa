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
                    request.ID = f.ID;
                    requests.Add(request);
                }
            return View(requests);
                
            /*if (string.IsNullOrEmpty(s.searchString) || string.IsNullOrWhiteSpace(s.searchString))
            {
                return RedirectToAction("Index");
            }

            var movies = from m in meditaTitleRepo.GetMovieTitles()
                         where m.title.Contains(s.searchString)
                         select m;
            if (movies == null)
            {
                return View("Error");
            }

            List<DisplayMovieView> ldmw = new List<DisplayMovieView>();
            foreach (var m in movies)
            {
                var files = from f in subtitleFileRepo.GetSubtitles()
                            where f.mediaTitleID == m.ID
                            select f;
                foreach (var f in files)
                {
                    DisplayMovieView dmw = new DisplayMovieView();
                    dmw.title = m.title;
                    dmw.yearReleased = m.yearReleased;
                    dmw.userName = f.userName;
                    dmw.language = f.language;
                    dmw.date = f.date;
                    dmw.downloadCount = f.downloadCount;
                    ldmw.Add(dmw);
                }
            }
            return View(ldmw);*/
        }
        public ActionResult CreateRequest()
        {
            return View();
        }
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
	}
}