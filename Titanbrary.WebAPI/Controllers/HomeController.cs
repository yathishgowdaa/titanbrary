using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Titanbrary.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Titanbrary";
          
            return View();
        }

       
        public ActionResult SignUp()
        {
            ViewBag.Title = "Sign Up!";
            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Title = "Sign In!";
            return View();
        }
    }
}
