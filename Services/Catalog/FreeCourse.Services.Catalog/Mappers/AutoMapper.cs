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
            CreateMap<CategoryEntity, CategoryResponse>();
            CreateMap<CreateCategoryRequest, CategoryEntity>();
            
            CreateMap<FeatureEntity, FeatureResponse>();
            CreateMap<FeatureRequest, FeatureEntity>();

            CreateMap<CourseEntity, CourseResponse>();
            CreateMap<CreateCourseRequest, CourseEntity>();
            CreateMap<UpdateCourseRequest, CourseEntity>();
        }
    }
}