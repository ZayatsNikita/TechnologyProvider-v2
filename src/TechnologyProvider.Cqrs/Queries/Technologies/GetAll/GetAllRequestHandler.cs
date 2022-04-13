using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetAll
{
    /// <summary>
    /// Get all request handler.
    /// </summary>
    public class GetAllRequestHandler : IRequestHandler<GetAllRequest, Result<IEnumerable<TechnologyResponseModel>>>
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
        /// <returns>A result object that contains the extracted values or an error message.</returns>
        public Task<Result<IEnumerable<TechnologyResponseModel>>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var result = this.dbContext.Technologies.AsNoTracking()
                .Select(x => new TechnologyResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).AsEnumerable();

            return Task.FromResult(Result<IEnumerable<TechnologyResponseModel>>.Success(result));
        }
    }
}
