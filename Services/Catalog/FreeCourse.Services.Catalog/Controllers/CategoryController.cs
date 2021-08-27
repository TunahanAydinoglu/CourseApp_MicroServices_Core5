using System.Threading;
using System.Threading.Tasks;
using FreeCourse.CoreLib.Utils;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var response = await _categoryServices.GetAllAsync(cancellationToken);

            return CustomActionResult(response);
        }


        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(string categoryId, CancellationToken cancellationToken)
        {
            var response = await _categoryServices.GetByIdAsync(categoryId, cancellationToken);

            return CustomActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest requestModel, CancellationToken cancellationToken)
        {
            var response = await _categoryServices.CreateAsync(requestModel, cancellationToken);

            return CustomActionResult(response);
        }
    }
}