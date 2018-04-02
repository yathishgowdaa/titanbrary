using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Titanbrary.BusinessObjects;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;

namespace Titanbrary.WebAPI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private ApplicationUserManager _UserManager;
        //private readonly IAccount _Account;

        private AccountManager accountMgr = new AccountManager();

        public DashboardController() { }
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

        //public DashboardController(IAccount _account)
        //{
        //    _Account = _account;
        //}
        public ActionResult Index()
        {
            var result = new UserModel();
            try
            {
                var currentUser = UserManager.FindByEmail(User.Identity.Name);
               

                if (currentUser != null)
                {
                    UserModel userInfo = accountMgr.GetUserInfo(currentUser);
                    if(userInfo == null)
                    {
                        return RedirectToAction("SignIp", "Home");
                    }
                   result = userInfo;              
                }     
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View(result);
        }
    }
}