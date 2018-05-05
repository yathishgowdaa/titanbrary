using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Titanbrary.Common.Interfaces.BusinessObjects;
using Titanbrary.Common.Models;
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() {
                UserName = model.Email,
                Email = model.Email,
                Password = model.Password,
                UserRoles = "Customer"
            };

            var result = await UserManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                return ResponseMessage(response);  
            }
            var userInfo = UserManager.FindByEmail(model.Email);
            await UserManager.AddToRoleAsync(userInfo.Id, user.UserRoles);

            return Ok("Account was created");
        }


        [HttpPost]
        [Authorize]
        [Route ("Test")]
        public IHttpActionResult TestMyApI()
        {
            return Ok("You were authorized");
        }
    }
}
