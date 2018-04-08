using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [Authorize(Roles = "Admin, Manager, Customer")]
        public ActionResult Index()
        {
            var result = new UserModel();
            try
            {
                //check if authenticated
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("SignIp", "Home");
                }
                //get user role
                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                var roles = UserManager.GetRoles(currentUser.Id);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                //redirect to dashboard
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        return View("Admin", userInfo);

                    }
                    else if (role == "Manager")
                    {
                        return View("Manager", userInfo);

                    }
                    else
                    {
                        return View(userInfo);
                    }
                }

                //if (currentUser != null)
                //{
                //    UserModel userInfo = accountMgr.GetUserInfo(currentUser);
                //    if(userInfo == null)
                //    {
                //        return RedirectToAction("SignIp", "Home");
                //    }
                //   result = userInfo;              
                //}     
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View(result);
        }

        public ActionResult CreateUser()
        {
            //return the form to create a user with role
            //use API to register?
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult BookView()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            return View("Book/Index", userInfo);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateBook()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            return View("Book/Create", userInfo);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateBook()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);

            return View("Book/Update", userInfo);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateBookById(Guid bookId)
        {
            

            //api call
            var data = new Dictionary<string, string>
                   {
                       {"bookId", bookId.ToString()}
                   };

            
            using (HttpClient httpClient = new HttpClient())
            {
                var currentUser = UserManager.FindByEmail(User.Identity.Name);
                UserModel userInfo = accountMgr.GetUserInfo(currentUser);

                var getTokenUrl = httpClient.GetAsync(String.Format("http://localhost:50799/api/Book/GetBookByBookID/{0}", bookId.ToString()));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));
                if (getTokenUrl.IsCompleted)
                {
                    var response = getTokenUrl.Result.Content.ReadAsStringAsync().Result;
                    
                    var model = new UserInfoBookModel();
                    model.book = JsonConvert.DeserializeObject<BookModel>(response);
                    model.user = userInfo;

                    return View("Book/BookProfile", model);
                }
                
            }

            //return back to book list
            return RedirectToAction("UpdateBook", "Dashboard");
            
            
            
            
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateGenre()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            return View("Genre/Create", userInfo);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateGenre()
        {
            var currentUser = UserManager.FindByEmail(User.Identity.Name);
            UserModel userInfo = accountMgr.GetUserInfo(currentUser);
            return View("Genre/Update", userInfo);
        }
    }
}