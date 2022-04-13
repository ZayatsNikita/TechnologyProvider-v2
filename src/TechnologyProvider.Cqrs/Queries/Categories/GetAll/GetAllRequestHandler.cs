using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetAll
{
    /// <summary>
    /// The handler responsible for getting all categories.
    /// </summary>
    public class GetAllRequestHandler : IRequestHandler<GetAllRequest, Result<IEnumerable<CategoryResponseModel>>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        public GetAllRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing collection of categories or an error message.</returns>
        public Task<Result<IEnumerable<CategoryResponseModel>>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var categories = this.dbContext.Categories.AsNoTracking()
                .Select(x => new CategoryResponseModel
                {
                    Name = x.Name,
                    Id = x.Id,
                }).AsEnumerable();

            return Task.FromResult(Result<IEnumerable<CategoryResponseModel>>.Success(categories));
        }
    }
}
