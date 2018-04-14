using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Titanbrary.BusinessObjects;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;
using Titanbrary.WebAPI.Models;
using System.Net.Mail;
using System.Net;

namespace Titanbrary.WebAPI.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationUserManager _UserManager;
        private ApplicationSignInManager _SignInManager;

        //private readonly IAccount account;

        private AccountManager accountMgr = new AccountManager();
        

        public HomeController()
        {
           
        }

        //public HomeController(IAccount _account)
        //{
        //    account = _account;
        //}
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

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _SignInManager ?? Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set
            {
                _SignInManager = value;
            }
        }


       

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

        //Register
        [HttpPost]
        public async Task<ActionResult> SignUp(UserModel model)
        {
            ViewBag.Title = "Sign Up!";
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            if (model.Email != model.Email2)// && model.Password != model.Password2)
            {
                ModelState.AddModelError("Emails", "Email is not macthing");
                if (model.Password != model.Password2)
                {
                    ModelState.AddModelError("Passwords", "Password is not matching");
                    return View(model);
                }
                return View(model);
            }
            else if (model.Password != model.Password2)
            {

                ModelState.AddModelError("Passwords", "Password is not matching");
                return View(model);

            }

            //create ASP.NET USER
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                //Password = model.Password,
                UserRoles = "Customer"
            };

            var result = await UserManager.CreateAsync(user,model.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Account could not be created. Please try again");
                return View();
            }

            var userInfo = await UserManager.FindByEmailAsync(model.Email);
            var addRole = await UserManager.AddToRoleAsync(userInfo.Id, user.UserRoles);
            if (!addRole.Succeeded)
            {
                ModelState.AddModelError("Error", "Account could not be created. Please try again");
                return View();
            }
            //Save to UserAccount Table
            model.Id = userInfo.Id;
            var acc = accountMgr.SaveAccount(model);
            if(!acc)
            {
                return Redirect("SignUp");
            }


            return Redirect("SignIn");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {

            Dictionary<string, string> tokenDetails = null;
            using (HttpClient httpClient = new HttpClient())
            {

                var login = new Dictionary<string, string>
                   {
                       {"grant_type", "password"},
                       {"username", model.Email},
                       {"password", model.Password},
                   };

                var getTokenUrl = httpClient.PostAsync("http://localhost:50799/token", new FormUrlEncodedContent(login));
                getTokenUrl.Wait(TimeSpan.FromSeconds(10));

                if (getTokenUrl.IsCompleted)
                {

                    if (getTokenUrl.Result.Content.ReadAsStringAsync().Result.Contains("access_token"))
                    {
                        tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(getTokenUrl.Result.Content.ReadAsStringAsync().Result);
                    }
                }

                AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(tokenDetails["expires_in"]));
                
                var userInfo = UserManager.FindByEmail(model.Email);
              
                //var role = await UserManager.GetRolesAsync()
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim("Bearer", string.Format("{0}", tokenDetails["access_token"])),
                    new Claim(ClaimTypes.Role, userInfo.UserRoles)

                };
                
                //save to cookie
                HttpCookie TokenCookies = new HttpCookie("AuthenticationToken");
                TokenCookies.Value = claims[1].ToString();
                TokenCookies.Expires = DateTime.UtcNow.AddDays(1);
                HttpContext.Response.Cookies.Add(TokenCookies);

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                Request.GetOwinContext().Authentication.SignIn(options, identity);

            }
            

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            if (Request.Cookies["AuthenticationToken"] != null)
            {
                var c = new HttpCookie("AuthenticationToken");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Test()
        {
            return View();
        }

        public async void RegistrationEmail(UserModel model)
        {
            SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
            mailClient.Credentials = new NetworkCredential("titanbrary.reminders@gmail.com", "titanbraryreminders");

            MailMessage email = new MailMessage();
            email.From = new MailAddress("Titanbrary@gmail.com");
            email.To.Add(model.Email);
            email.Subject = "Account Registered!";
            email.Body = string.Format("<p>Hello {0} {1}</p><p>Thank you for signing up!</p><p>Thanks,</p><p>Titanbrary Team</p>", model.FirstName, model.LastName);

            await mailClient.SendMailAsync(email);
        }
    }
}
