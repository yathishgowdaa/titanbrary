using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Titanbrary.Common.Interfaces.BusinessObjects;

namespace Titanbrary.WebAPI.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ApplicationUserManager _UserManager;
        private readonly IAccount _userSession;


        public TestController()
        { }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _UserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _UserManager = value;
            }
        }
        public TestController(IAccount userSession)
        {
            _userSession = userSession;
        }

        // GET: Home
        
        public ActionResult Index()
        {
            var user = UserManager.FindByName(User.Identity.Name);
            var role = user.Roles;
            return View();

           
        }
        
    }
}