using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetById
{
    public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, Result<CategoryModel>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public GetByIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CategoryModel>> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            return category is null
                ? Result<CategoryModel>.NotFound("", nameof(request.Id))
                : Result<CategoryModel>.Success(new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                });
        }
    }
}
