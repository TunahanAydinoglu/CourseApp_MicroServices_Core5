using System;
using FreeCourse.Services.Catalog.Models.Response;

namespace FreeCourse.Services.Catalog.Models.Request
{
    public class AddCourseRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public FeatureEntity FeatureEntity { get; set; }
        public string CategoryId { get; set; }
    }
}