using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.WebAPI.Models;


namespace Titanbrary.WebAPI.Controllers.API
{
    [RoutePrefix("api/Registration")]
    public class RegistrationController : ApiController
    {
        private ApplicationUserManager _UserManager;
        private readonly IAccount _Account;

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

        public RegistrationController(IAccount _account)
        {
            _Account = _account;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);

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
