using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Models;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    public class CreateTechnologyRequestHandler : IRequestHandler<CreateTechnologyRequest, Result<CreateTechnologyResultModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public CreateTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CreateTechnologyResultModel>> Handle(CreateTechnologyRequest request, CancellationToken cancellationToken)
        {
            if (await IsThereSameNameInTheStorage(request))
            {
                Result<CreateTechnologyResultModel>.ValidationFailed("", nameof(request.Name));
            }

            if (await AreRelevantCategoriesExist(request))
            {
                Result<CreateTechnologyResultModel>.ValidationFailed("", nameof(request.CategoryIds));
            }

            var technology = new Technology
            {
                Description = request.Description,
                Name = request.Name,
            };

            _dbContext.Set<Technology>().Add(technology);

            _dbContext.Set<TechnologyCategory>().AddRange(request.CategoryIds.Select(x => new TechnologyCategory
            {
                TechnologyId = technology.Id,
                CategoryId = x,
            }));

            await _dbContext.SaveChangesAsync();

            var resultModel = new CreateTechnologyResultModel
            {
                Description = technology.Description,
                Name = technology.Name,
                CategoryIds = request.CategoryIds,
                Id = technology.Id,
            };

            return Result<CreateTechnologyResultModel>.Success(resultModel);
        }

        private async Task<bool> IsThereSameNameInTheStorage(CreateTechnologyRequest request)
        {
            return await _dbContext.Set<Technology>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());
        }

        private async Task<bool> AreRelevantCategoriesExist(CreateTechnologyRequest request)
        {
            var ids = request.CategoryIds;

            var categoryCodes = await _dbContext.Set<Category>().AsNoTracking()
                .Select(x => x.Id)
                .ToArrayAsync();

            return ids.All(x => categoryCodes.Contains(x));
        }
    }
}
