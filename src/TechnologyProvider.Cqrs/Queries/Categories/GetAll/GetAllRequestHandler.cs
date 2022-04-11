using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetAll
{
    internal class GetAllRequestHandler : IRequestHandler<GetAllRequest, Result<IEnumerable<CategoryModel>>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public GetAllRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Result<IEnumerable<CategoryModel>>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var categories = _dbContext.Categories.AsNoTracking()
                .Select(x => new CategoryModel
                {
                    Name = x.Name,
                    Id = x.Id,
                }).AsEnumerable();

            return Task.FromResult(Result<IEnumerable<CategoryModel>>.Success(categories));
        }
    }
}
