using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Titanbrary.WebAPI.Controllers
{
    public class HelpsController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            ViewBag.Title = "Help Page";
            return View();
        }
    }
}