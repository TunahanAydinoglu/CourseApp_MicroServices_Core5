using FluentValidation;
using FreeCourse.CoreLib.Utils;

namespace FreeCourse.Services.Catalog.Models.Request
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public FeatureRequest FeatureRequest { get; set; }
        public string CategoryId { get; set; }
    }

    public class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Picture).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            
            RuleFor(x => x.UserId).NotEmpty().Must(CustomValidators.IsObjectId).WithMessage("UserId objectId tipinde olmalidir.");
            RuleFor(x => x.CategoryId).NotEmpty().Must(CustomValidators.IsObjectId).WithMessage("CategoryId objectId tipinde olmalidir.");
        }

        
    }
    
}