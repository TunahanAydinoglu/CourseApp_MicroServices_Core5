using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.CoreLib.BaseModels;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Models.Response;

namespace FreeCourse.Services.Catalog.Services.Course
{
    public interface ICourseServices
    {
        Task<BaseResponse<NoContent>> CreateAsync(CreateCourseRequest requestModel, CancellationToken cancellationToken);
        Task<BaseResponse<List<CourseResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken);
        Task<BaseResponse<CourseResponse>> GetByIdAsync(string categoryId, CancellationToken cancellationToken);
        Task<BaseResponse<NoContent>> UpdateAsync(string courseId, UpdateCourseRequest updateModel, CancellationToken cancellationToken);
        Task<BaseResponse<NoContent>> DeleteAsync(string courseId, CancellationToken cancellationToken);
    }
}