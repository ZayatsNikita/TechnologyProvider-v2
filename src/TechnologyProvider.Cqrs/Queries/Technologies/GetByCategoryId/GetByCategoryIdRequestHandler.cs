using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId
{
    /// <summary>
    /// Request handler for getting technologies by category.
    /// </summary>
    public class GetByCategoryIdRequestHandler : IRequestHandler<GetByCategoryIdRequest, Result<IEnumerable<TechnologyResponseModel>>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByCategoryIdRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        public GetByCategoryIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing technology collection or an error message.</returns>
        public async Task<Result<IEnumerable<TechnologyResponseModel>>> Handle(GetByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var category = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (category is null)
            {
                return Result<IEnumerable<TechnologyResponseModel>>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id));
            }
            else
            {
                var technologies = this.dbContext.TechnologyCategories.AsNoTracking()
                    .Where(x => x.CategoryId == category.Id)
                    .Join(
                        this.dbContext.Technologies.AsNoTracking(),
                        tc => tc.TechnologyId,
                        t => t.Id,
                        (tc, t) => new TechnologyResponseModel
                        {
                            Name = t.Name,
                            Id = t.Id,
                            Description = t.Description,
                        }).AsEnumerable();

                return Result<IEnumerable<TechnologyResponseModel>>.Success(technologies);
            }
        }
    }
}
