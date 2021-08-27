using FluentValidation;

namespace FreeCourse.Services.Catalog.Models.Request
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
    }
    
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}