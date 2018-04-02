using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Titanbrary.WebAPI.Models;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Titanbrary.Common.Models;

[assembly: OwinStartup(typeof(Titanbrary.WebAPI.Startup))]

namespace Titanbrary.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
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
        }

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
                    var role = new RoleModel();
                    role.Name = "Admin";
                    role.RoleId = Guid.NewGuid().ToString();
                    role.RoleName = role.Name;
                    roleManager.Create(role);

                    //create admin account
                    var user = new ApplicationUser();
                    user.UserName = "admin";
                    user.Email = "admin@titanbrary.com";
                    //create a password 
                    string pwd = "admin";
                    var adminUser = UserManager.Create(user, pwd);

                    //add default user to admin role
                    if (adminUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(user.Id, "Admin");
                    }

                }

                //Create Manager role
                if (!roleManager.RoleExists("Manager"))
                {
                    var role = new RoleModel();
                    role.Name = "Manager";
                    role.RoleId = Guid.NewGuid().ToString();
                    role.RoleName = role.Name;

                    roleManager.Create(role);
                }

                //Create Customer role
                if (!roleManager.RoleExists("Customer"))
                {
                    var role = new RoleModel();
                    role.Name = "Customer";
                    role.RoleId = Guid.NewGuid().ToString();
                    role.RoleName = role.Name;
                    roleManager.Create(role);
                }

            }
            


            
        }
    } 
}
