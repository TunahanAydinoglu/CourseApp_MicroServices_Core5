using FreeCourse.CoreLib.BaseModels;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.CoreLib.Utils
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomActionResult<T>(BaseResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}