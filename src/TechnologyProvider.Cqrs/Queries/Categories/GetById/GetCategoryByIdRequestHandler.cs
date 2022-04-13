using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetById
{
    /// <summary>
    /// The handler responsible for getting category by id.
    /// </summary>
    public class GetCategoryByIdRequestHandler : IRequestHandler<GetCategoryByIdRequest, Result<CategoryResponseModel>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        public GetCategoryByIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing a category or an error message.</returns>
        public async Task<Result<CategoryResponseModel>> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await this.dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return category is null
                ? Result<CategoryResponseModel>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id))
                : Result<CategoryResponseModel>.Success(new CategoryResponseModel
                {
                    Id = category.Id,
                    Name = category.Name,
                });
        }
    }
}
