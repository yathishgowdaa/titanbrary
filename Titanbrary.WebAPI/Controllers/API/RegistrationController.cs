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
    [RoutePrefix("Account")]
    public class RegistrationController : ApiController
    {
        private ApplicationUserManager _userManager;

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
        [Route("Registration")]
        public async Task<IHttpActionResult> Register(ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                return ResponseMessage(response);  
            }
            
            await UserManager.AddToRoleAsync(user.Id, model.UserRoles);

            return Ok("Account was created");
        }
    }
}
