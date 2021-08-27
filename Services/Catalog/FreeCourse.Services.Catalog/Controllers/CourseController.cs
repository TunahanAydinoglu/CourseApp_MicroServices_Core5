using System.Threading;
using System.Threading.Tasks;
using FreeCourse.CoreLib.Utils;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Services.Course;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCources([FromQuery] string userId, CancellationToken cancellationToken)
        {
            var response = await _courseServices.GetAllAsync(userId, cancellationToken);

            return CustomActionResult(response);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(string courseId, CancellationToken cancellationToken)
        {
            var response = await _courseServices.GetByIdAsync(courseId, cancellationToken);

            return CustomActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseRequest requestModel, CancellationToken cancellationToken)
        {
            var response = await _courseServices.CreateAsync(requestModel, cancellationToken);

            return CustomActionResult(response);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(string courseId, UpdateCourseRequest requestModel, CancellationToken cancellationToken)
        {
            var response = await _courseServices.UpdateAsync(courseId, requestModel, cancellationToken);

            return CustomActionResult(response);
        }


        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(string courseId, CancellationToken cancellationToken)
        {
            var response = await _courseServices.DeleteAsync(courseId, cancellationToken);

            return CustomActionResult(response);
        }
    }
}