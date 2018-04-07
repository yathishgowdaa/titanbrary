using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Titanbrary.WebAPI.Models;
using Titanbrary.Data;

[assembly: OwinStartup(typeof(Titanbrary.WebAPI.Startup))]

namespace Titanbrary.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => deleteOldCarts(), Cron.Daily);

            ConfigureAuth(app);
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
                    var role = new IdentityRole();
                    role.Name = "Admin";
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
                    var role = new IdentityRole();
                    role.Name = "Manager";
                    roleManager.Create(role);
                }

                //Create Customer role
                if (!roleManager.RoleExists("Customer"))
                {
                    var role = new IdentityRole();
                    role.Name = "Customer";
                    roleManager.Create(role);
                }

            }
            


            
        }

        private void deleteOldCarts()
        {
            using (TitanbraryEntities ctx = new TitanbraryEntities())
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
