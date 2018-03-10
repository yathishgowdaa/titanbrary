using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Titanbrary.WebAPI.Models;

namespace Titanbrary.WebAPI.Controllers.API
{
    [RoutePrefix("Role")]
    public class RoleController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public async Task<IHttpActionResult> CreateRole(RoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleName = new ApplicationRole() { Name = model.RoleName };
            await RoleManager.CreateAsync(roleName);
           

            return Ok("Role was created");
        }
    }
}