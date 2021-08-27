using AutoMapper;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Models.Response;

namespace FreeCourse.Services.Catalog.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CourseEntity, CategoryResponse>();
            CreateMap<CategoryEntity, CategoryResponse>();
            CreateMap<FeatureEntity, FeatureResponse>();

            CreateMap<CreateCourseRequest, CourseEntity>();
            CreateMap<UpdateCourseRequest, CourseEntity>();
        }
    }
}