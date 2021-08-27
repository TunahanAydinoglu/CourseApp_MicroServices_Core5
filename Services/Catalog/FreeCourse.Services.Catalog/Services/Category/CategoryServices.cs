using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FreeCourse.CoreLib.BaseModels;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Models.Request;
using FreeCourse.Services.Catalog.Models.Response;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    internal class CategoryServices : ICategoryServices
    {
        private readonly IMongoCollection<CategoryEntity> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryServices(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = db.GetCollection<CategoryEntity>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<BaseResponse<NoContent>> CreateAsync(CreateCategoryRequest requestModel, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(requestModel);
            await _categoryCollection.InsertOneAsync(categoryEntity, cancellationToken: cancellationToken);
            return BaseResponse<NoContent>.Success(201);
        }
        
        public async Task<BaseResponse<List<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var dbCategories = await _categoryCollection.Find(c => true).ToListAsync(cancellationToken: cancellationToken);

            var responseData = _mapper.Map<List<CategoryResponse>>(dbCategories);
            var response = BaseResponse<List<CategoryResponse>>.Success(responseData,200);

            return response;
        }

        public async Task<BaseResponse<CategoryResponse>> GetByIdAsync(string categoryId, CancellationToken cancellationToken)
        {
            var dbCategory = await _categoryCollection.Find<CategoryEntity>(c => c.Id == categoryId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (dbCategory == null) return BaseResponse<CategoryResponse>.Error("Category not found!", 404);
            
            var responseData = _mapper.Map<CategoryResponse>(dbCategory);
            var response = BaseResponse<CategoryResponse>.Success(responseData,200);

            return response;
        }
    }
}