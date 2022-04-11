using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetById
{
    internal class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<TechnologyModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public GetByIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TechnologyModel>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var technology = await _dbContext.Technologies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (technology is null)
            {
                return Result<TechnologyModel>.NotFound("", nameof(request.Id));
            }
            else
            {
                var categories = await _dbContext.TechnologyCategories.AsNoTracking()
                    .Where(x => x.TechnologyId == technology.Id)
                    .Join(_dbContext.Categories,
                    tc => tc.CategoryId,
                    c => c.Id,
                    (tc, c) => new CategoryModel
                    {
                        Id = tc.CategoryId,
                        Name = c.Name,
                    }).ToListAsync();

                return Result<TechnologyModel>.Success(new TechnologyModel
                {
                    Name = technology.Name,
                    Description = technology.Description,
                    Id = technology.Id,
                    Categories = categories
                });
            }
        }
    }
}
