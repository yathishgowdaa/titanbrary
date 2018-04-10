using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Titanbrary.Common.Models;

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
