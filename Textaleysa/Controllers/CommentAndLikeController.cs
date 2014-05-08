using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;

namespace Textaleysa.Controllers
{
    public class CommentAndLikeController : Controller
    {
        //
        // GET: /CommentAndLike/
        public ActionResult Index()
        {
            return View();
        }

		// Gets all the comments
		public ActionResult GetComments()
		{
			return View();
		}

		// Posts a new comment
		public ActionResult PostComment(Comment comment)
		{
			return View();
		}


		public ActionResult GetLikes()
		{
			return View();
		}

		public ActionResult PostLike(Like like)
		{
			return View();
		}
	}
}