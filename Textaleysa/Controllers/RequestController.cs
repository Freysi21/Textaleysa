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
	}
}