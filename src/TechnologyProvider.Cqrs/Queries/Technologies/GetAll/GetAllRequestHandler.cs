using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetAll
{
    public class GetAllRequestHandler : IRequestHandler<GetAllRequest, Result<IEnumerable<TechnologyModel>>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public GetAllRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<TechnologyModel>>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories.AsNoTracking()
                .Select(x => new CategoryModel
                {
                    Name = x.Name,
                    Id = x.Id,
                }).ToArrayAsync();

            var result = _dbContext.Technologies.AsNoTracking()
                .GroupJoin(_dbContext.TechnologyCategories,
                    t => t.Id,
                    tc => tc.TechnologyId,
                    (t, tc) => new TechnologyModel
                    {
                        Description = t.Description,
                        Name = t.Name,
                        Id = t.Id,
                        Categories = categories.Where(z => tc.Any(tcPair => tcPair.CategoryId == z.Id))
                        .ToList()
                    }).AsEnumerable();

            return Result<IEnumerable<TechnologyModel>>.Success(result);
        }
    }
}
