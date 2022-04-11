using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Models;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Result<CreateCategoryResultModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public CreateCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CreateCategoryResultModel>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            _dbContext.Set<Category>().Add(category);

            await _dbContext.SaveChangesAsync();

            var resultModel = new CreateCategoryResultModel
            {
                Name = category.Name,
                Id = category.Id,
            };

            return Result<CreateCategoryResultModel>.Success(resultModel);
        }
    }
}
