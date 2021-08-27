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
    internal class CourseServices : ICourseServices
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

        public async Task<BaseResponse<NoContent>> CreateAsync(CreateCourseRequest requestModel, CancellationToken cancellationToken)
        {
            var dbCategory = await _categoryCollection.Find(c => c.Id == requestModel.CategoryId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (dbCategory == null)
            {
                return BaseResponse<NoContent>.Error("Category is not exist!", 400);
            }

            var courseEntity = _mapper.Map<CourseEntity>(requestModel);
            courseEntity.CreatedDate = DateTime.Now;

            await _courseCollection.InsertOneAsync(courseEntity, cancellationToken: cancellationToken);
            return BaseResponse<NoContent>.Success(201);
        }

        public async Task<BaseResponse<List<CourseResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken)
        {
            var dbCourses = string.IsNullOrEmpty(userId)
                ? await _courseCollection.Find(c => true).ToListAsync(cancellationToken: cancellationToken)
                : await _courseCollection.Find(c => c.UserId == userId).ToListAsync(cancellationToken: cancellationToken);

            // List<CourseEntity> dbCourses;
            //
            // if (string.IsNullOrEmpty(userId))
            //     dbCourses = await _courseCollection.Find(c => true).ToListAsync(cancellationToken: cancellationToken);
            // else
            //     dbCourses = await _courseCollection.Find(c => c.UserId == userId).ToListAsync(cancellationToken: cancellationToken);

            if (dbCourses.Any())
            {
                foreach (var course in dbCourses)
                {
                    course.Category = await _categoryCollection.Find<CategoryEntity>(c => c.Id.Equals(course.CategoryId)).FirstOrDefaultAsync(cancellationToken);
                }
            }
            else
            {
                dbCourses = new List<CourseEntity>();
            }

            var responseData = _mapper.Map<List<CourseResponse>>(dbCourses);
            var response = BaseResponse<List<CourseResponse>>.Success(responseData, 200);

            return response;
        }

        public async Task<BaseResponse<CourseResponse>> GetByIdAsync(string categoryId, CancellationToken cancellationToken)
        {
            var dbCategory = await _courseCollection.Find<CourseEntity>(c => c.Id == categoryId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (dbCategory == null) return BaseResponse<CourseResponse>.Error("Course not found!", 404);

            dbCategory.Category = await _categoryCollection.Find(x => x.Id == dbCategory.CategoryId).FirstOrDefaultAsync(cancellationToken);

            var responseData = _mapper.Map<CourseResponse>(dbCategory);
            var response = BaseResponse<CourseResponse>.Success(responseData, 200);

            return response;
        }

        public async Task<BaseResponse<NoContent>> UpdateAsync(string courseId, UpdateCourseRequest updateModel, CancellationToken cancellationToken)
        {
            var updateCourse = _mapper.Map<CourseEntity>(updateModel);
            updateCourse.Id = courseId;

         
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id.Equals(courseId), updateCourse, cancellationToken: cancellationToken);

            if (result == null)
            {
                return BaseResponse<NoContent>.Error("Course not found", 404);
            }

            return BaseResponse<NoContent>.Success(204);
        }

        public async Task<BaseResponse<NoContent>> DeleteAsync(string courseId, CancellationToken cancellationToken)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id.Equals(courseId), cancellationToken: cancellationToken);

            return result.DeletedCount > 0 ? BaseResponse<NoContent>.Success(204) : BaseResponse<NoContent>.Error("Course not found", 404);
        }


        private async Task<bool> IsCategoryExist(string categoryId, CancellationToken cancellationToken)
        {
            var dbCategory = await _categoryCollection.Find(c => c.Id == updateModel.CategoryId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (dbCategory == null)
            {
                return BaseResponse<NoContent>.Error("Category is not exist!", 400);
            }
        }
    }
}