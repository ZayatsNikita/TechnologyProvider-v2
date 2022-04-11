using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Models;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    public class UpdateTechnologyRequestHandler : IRequestHandler<UpdateTechnologyRequest, Result<UpdateTechonologyResultModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public UpdateTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<UpdateTechonologyResultModel>> Handle(UpdateTechnologyRequest request, CancellationToken cancellationToken)
        {
            var oldVersion = await _dbContext.Set<Technology>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (oldVersion == null)
            {
                return Result<UpdateTechonologyResultModel>.NotFound("", nameof(request.Id));
            }

            if (await IsThereSameNameInTheStorage(request, oldVersion))
            {
                Result<UpdateTechonologyResultModel>.ValidationFailed("", nameof(request.Name));
            }

            if (await AreRelevantCategoriesExist(request))
            {
                Result<UpdateTechonologyResultModel>.ValidationFailed("", nameof(request.CategoryIds));
            }

            var oldTechnologyCategories = _dbContext.Set<TechnologyCategory>().AsNoTracking()
                .Where(x => x.TechnologyId == oldVersion.Id)
                .ToArray();

            await UpdateTechnologyCategory(request.CategoryIds, oldTechnologyCategories, request.Id);

            oldVersion.Name = request.Name;
            oldVersion.Description = request.Description;

            await _dbContext.SaveChangesAsync();

            var resultModel = new UpdateTechonologyResultModel
            {
                Description = request.Description,
                Name = request.Name,
                CategoryIds = request.CategoryIds,
                Id = request.Id,
            };

            return Result<UpdateTechonologyResultModel>.Success(resultModel);
        }

        private async Task UpdateTechnologyCategory(IEnumerable<int> newCategoryCodes, IEnumerable<TechnologyCategory> oldTechnologyCategories, int technologyId)
        {
            var newTechnologyCategories = newCategoryCodes.Select(x => new TechnologyCategory
            {
                CategoryId = x,
                TechnologyId = technologyId
            });

            var toDelete = oldTechnologyCategories.Where(x => newCategoryCodes.All(z => z != x.CategoryId));

            var toAdd = newTechnologyCategories.Where(x => oldTechnologyCategories.All(z => z.CategoryId != x.CategoryId));

            _dbContext.Set<TechnologyCategory>().RemoveRange(toDelete);
            await _dbContext.Set<TechnologyCategory>().AddRangeAsync(toAdd);
        }

        private async Task<bool> IsThereSameNameInTheStorage(UpdateTechnologyRequest request, Technology oldVersion)
        {
            if (oldVersion.Name == request.Name)
            {
                return false;
            }
            else
            {
                return await _dbContext.Set<Technology>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());
            }
        }

        private async Task<bool> AreRelevantCategoriesExist(UpdateTechnologyRequest request)
        {
            var ids = request.CategoryIds;

            var categoryCodes = await _dbContext.Categories.AsNoTracking()
                .Select(x => x.Id)
                .ToArrayAsync();

            return ids.All(x => categoryCodes.Contains(x));
        }
    }
}
