using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Models;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Result<UpdateCategoryResultModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public UpdateCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<UpdateCategoryResultModel>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var oldVersion = await _dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (oldVersion == null)
            {
                return Result<UpdateCategoryResultModel>.NotFound("", nameof(request.Id));
            }

            oldVersion.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            var resultModel = new UpdateCategoryResultModel
            {
                Name = request.Name,
                Id = request.Id,
            };

            return Result<UpdateCategoryResultModel>.Success(resultModel);
        }
    }
}
