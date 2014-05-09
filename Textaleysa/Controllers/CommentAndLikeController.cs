using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textaleysa.DAL;
using Textaleysa.Models;
using Textaleysa.Models.Repositories;

namespace Textaleysa.Controllers
{
    public class CommentAndLikeController : Controller
    {
		private HRContext db = new HRContext();
		CommentRepository repo = new CommentRepository();

        public ActionResult Index()
        {
            return View();
        }

		// Gets all the comments
		public ActionResult GetComments()
		{
			// get all comments
			var comments = repo.GetComments();
			var result = from c in comments
						 select new
						 {
							 commentDate = c.date.ToString("dd. MMMM HH:mm"),
							 ID = c.ID,
							 commentContent = c.content,
							 userName = c.userName
						 };
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		// Posts a new comment
		public ActionResult PostComment(Comment comment)
		{
			Comment c = new Comment();
			// get the user name
			var userName = User.Identity.Name;
			c.userName = userName;
			c.content = comment.content;
			c.date = DateTime.Now;
			repo.AddComment(c);
			return Json(c, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetLikes()
		{
			return View();
		}

		public ActionResult PostLike(Like like)
		{
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}