using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FreeCourse.CoreLib.BaseModels;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Models.Response;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services.Course
{
    internal class CourseServices
    {
        private readonly IMongoCollection<CourseEntity> _courseCollection;
        private readonly IMongoCollection<CategoryEntity> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = db.GetCollection<CourseEntity>(databaseSettings.CourseCollectionName);
            _categoryCollection = db.GetCollection<CategoryEntity>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        
        
        public async Task<BaseResponse<NoContent>> CreateAsync(AddCourseRequest requestModel, CancellationToken cancellationToken)
        {
            var courseModel = _mapper.Map<CourseEntity>(requestModel);
            courseModel.CreatedDate = DateTime.Now;
            
            await _courseCollection.InsertOneAsync(courseModel, cancellationToken: cancellationToken);
            return BaseResponse<NoContent>.Success(201);
        }
        
        public async Task<BaseResponse<List<CourseResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var dbCourses = await _courseCollection.Find(c => true).ToListAsync(cancellationToken: cancellationToken);

            if (dbCourses.Any())
            {
                foreach (var course in dbCourses)
                {
                    course.Category= await _categoryCollection.Find<CategoryEntity>(c => c.Id == course.CategoryId).FirstAsync(cancellationToken);
                }
            }
            else
            {
                dbCourses = new List<CourseEntity>();
            }
            
            var responseData = _mapper.Map<List<CourseResponse>>(dbCourses);
            var response = BaseResponse<List<CourseResponse>>.Success(responseData,200);

            return response;
        }

        public async Task<BaseResponse<CourseResponse>> GetByIdAsync(string categoryId, CancellationToken cancellationToken)
        {
            var dbCategory = await _courseCollection.Find<CourseEntity>(c => c.Id == categoryId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (dbCategory == null) return BaseResponse<CourseResponse>.Error("Course not found!", 404);

            dbCategory.Category = await _categoryCollection.Find(x => x.Id == dbCategory.CategoryId).FirstAsync(cancellationToken);
            
            var responseData = _mapper.Map<CourseResponse>(dbCategory);
            var response = BaseResponse<CourseResponse>.Success(responseData,200);

            return response;
        }
        
        public async Task<BaseResponse<List<CourseResponse>>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var dbCourses = await _courseCollection.Find(c => c.UserId == userId).ToListAsync(cancellationToken: cancellationToken);

            if (dbCourses.Any())
            {
                foreach (var course in dbCourses)
                {
                    course.Category= await _categoryCollection.Find<CategoryEntity>(c => c.Id == course.CategoryId).FirstAsync(cancellationToken);
                }
            }
            else
            {
                dbCourses = new List<CourseEntity>();
            }
            
            var responseData = _mapper.Map<List<CourseResponse>>(dbCourses);
            var response = BaseResponse<List<CourseResponse>>.Success(responseData,200);

            return response;
        }
    }
}