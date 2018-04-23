using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Hangfire;
using Titanbrary.WebAPI.Models;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Titanbrary.Common.Models;
using Titanbrary.BusinessObjects;
using Titanbrary.Data;

[assembly: OwinStartup(typeof(Titanbrary.WebAPI.Startup))]

namespace Titanbrary.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            Hangfire.GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => deleteOldCarts(), Cron.Daily);

            ConfigureAuth(app);
            //WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Home/SignIn"),
            });
            createRolesAndUsers();
            //ConfigureServices();
        }

        //private void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();

        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
        //    });
        //}

        // In this method we will create default User roles and Admin user for login 
        private void createRolesAndUsers()
        {
            

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

                // In Startup iam creating first Admin Role and creating a default Admin User 
                if (!roleManager.RoleExists("Admin"))
                {
                    //create admin role
                    //var role = new RoleModel();
                    //role.Name = "Admin";
                    //role.RoleId = Guid.NewGuid().ToString();
                    //role.RoleName = role.Name;
                    IdentityRole role = new IdentityRole("Admin");
                    
                    roleManager.Create(role);

                    //create admin account
                    var user = new ApplicationUser();
                    user.UserName = "admin@titanbrary.com";
                    user.Email = "admin@titanbrary.com";
                    //create a password 
                    string pwd = "Admin123!";
                    user.UserRoles = "Admin";
                    var adminUser = UserManager.Create(user, pwd);

                    //add default user to admin role
                    if (adminUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(user.Id, user.UserRoles);
                    }
                    using (TitanbraryContainer ct = new TitanbraryContainer())
                    {
                        //save to user account
                        var target = new User();
                        target.UserID = new Guid(user.Id);
                        target.FirstName = "John";
                        target.LastName = "Doe";
                        target.LoginName = user.Email;
                        target.Email = user.Email;
                        target.Password = "";
                        target.Address = "123 Avenue";
                        target.City = "Fullerton";
                        target.State = "CA";
                        target.ZipCode = "92832";
                        target.Phone = "1114445555";
                        target.SQAnwer1 = "yellow";
                        target.SQAnswer2 = "black";
                        target.SQAnswer3 = "red";
                        target.DateOfBirth = DateTime.Now;
                        target.MemberSince = DateTime.Now;

                        ct.Users.Add(target);
                        ct.SaveChanges();
                    }

                }

                //Create Manager role
                if (!roleManager.RoleExists("Manager"))
                {
                    //var role = new RoleModel();
                    //role.Name = "Manager";
                    //role.RoleId = Guid.NewGuid().ToString();
                    //role.RoleName = role.Name;
                    IdentityRole role = new IdentityRole("Manager");
                    roleManager.Create(role);

                    //create admin account
                    var user = new ApplicationUser();
                    user.UserName = "manager@titanbrary.com";
                    user.Email = "manager@titanbrary.com";
                    //create a password 
                    string pwd = "Manager123!";
                    user.UserRoles = "Manager";
                    var adminUser = UserManager.Create(user, pwd);

                    //add default user to admin role
                    if (adminUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(user.Id, user.UserRoles); 
                    }

                    using (TitanbraryContainer ct = new TitanbraryContainer())
                    {
                        //save to user account
                        var target = new User();
                        target.UserID = new Guid(user.Id);
                        target.FirstName = "Jane";
                        target.LastName = "Doe";
                        target.LoginName = user.Email;
                        target.Email = user.Email;
                        target.Password = "";
                        target.Address = "123 Avenue";
                        target.City = "Fullerton";
                        target.State = "CA";
                        target.ZipCode = "92832";
                        target.Phone = "1114445555";
                        target.SQAnwer1 = "yellow";
                        target.SQAnswer2 = "black";
                        target.SQAnswer3 = "red";
                        target.DateOfBirth = DateTime.Now;
                        target.MemberSince = DateTime.Now;

                        ct.Users.Add(target);
                        ct.SaveChanges();
                    }
                    
                }

                //Create Customer role
                if (!roleManager.RoleExists("Customer"))
                {
                    //var role = new RoleModel();
                    //role.Name = "Customer";
                    //role.RoleId = Guid.NewGuid().ToString();
                    //role.RoleName = role.Name;
                    IdentityRole role = new IdentityRole("Customer");
                    roleManager.Create(role);
                }

            }
            
            
        }

        public void deleteOldCarts()
        {
            using (TitanbraryContainer ctx = new TitanbraryContainer())
            {
                var carts = ctx.Carts.Where(c => c.ModifiedDate <= DateTime.Now.AddDays(-7) && c.Completed == false);
                foreach (var cart in carts)
                {
                    ctx.CartXBooks.RemoveRange(ctx.CartXBooks.Where(cb => cb.CartID == cart.CartID));
                }
                ctx.Carts.RemoveRange(carts);              
            }
        }
    } 
}
