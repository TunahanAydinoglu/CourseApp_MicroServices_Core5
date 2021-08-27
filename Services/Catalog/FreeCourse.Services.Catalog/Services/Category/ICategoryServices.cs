using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.CoreLib.BaseModels;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Models.Response;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryServices
    {
        Task<BaseResponse<NoContent>> CreateAsync(CreateCategoryRequest requestModel, CancellationToken cancellationToken);
        Task<BaseResponse<List<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<BaseResponse<CategoryResponse>> GetByIdAsync(string categoryId, CancellationToken cancellationToken);

    }
}