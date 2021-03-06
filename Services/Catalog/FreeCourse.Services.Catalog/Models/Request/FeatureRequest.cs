using System.Data;
using FluentValidation;

namespace FreeCourse.Services.Catalog.Models.Request
{
    public class FeatureRequest
    {
        public int Duration { get; set; }
    }

    public class FeatureRequestValidator : AbstractValidator<FeatureRequest>
    {
        public FeatureRequestValidator()
        {
            RuleFor(x => x.Duration).GreaterThan(0);
        }
    }
}