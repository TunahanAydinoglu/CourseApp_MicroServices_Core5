using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.CoreLib.BaseModels;
using FreeCourse.CoreLib.Utils;
using FreeCourse.IdentityServer.Models;
using FreeCourse.IdentityServer.Models.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel requestModel, CancellationToken cancellationToken)
        {
            var appUser = new ApplicationUser
            {
                UserName = requestModel.UserName,
                Email = requestModel.Email,
                City = requestModel.City
            };
            
            var result = await _userManager.CreateAsync(appUser, requestModel.Password);
            if(result.Succeeded)
                return BadRequest(BaseResponse<NoContent>.Error(result.Errors.Select(x=>x.Description).ToList(),400));

            return NoContent();
        }
    }
}