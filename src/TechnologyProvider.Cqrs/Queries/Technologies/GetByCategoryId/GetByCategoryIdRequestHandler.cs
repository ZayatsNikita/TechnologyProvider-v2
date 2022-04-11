using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId
{
    internal class GetByCategoryIdRequestHandler : IRequestHandler<GetByCategoryIdRequest, Result<IEnumerable<TechnologyModel>>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public GetByCategoryIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<TechnologyModel>>> Handle(GetByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
            {
                return Result<IEnumerable<TechnologyModel>>.NotFound("", nameof(request.Id));
            }
            else
            {
                var categories = await _dbContext.Categories.AsNoTracking().ToArrayAsync();

                var technologies = _dbContext.Technologies.GroupJoin(_dbContext.TechnologyCategories.Where(tc => tc.CategoryId == category.Id),
                    t => t.Id,
                    tc => tc.TechnologyId,
                    (t, tc) => new TechnologyModel
                    {
                        Name = t.Name,
                        Description = t.Description,
                        Id = t.Id,
                        Categories = categories.Where(c => tc.Any(x => x.CategoryId == c.Id))
                            .Select(x => new CategoryModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                            }).ToList(),
                    }).AsEnumerable();

                return Result<IEnumerable<TechnologyModel>>.Success(technologies);
            }
        }
    }
}
