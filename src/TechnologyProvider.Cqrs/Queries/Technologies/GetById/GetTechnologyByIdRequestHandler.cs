using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetById
{
    /// <summary>
    /// The handler responsible for getting technology by id.
    /// </summary>
    public class GetTechnologyByIdRequestHandler : IRequestHandler<GetTechnologyByIdRequest, Result<TechnologyResponseModel>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTechnologyByIdRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        public GetTechnologyByIdRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing a technology or an error message.</returns>
        public async Task<Result<TechnologyResponseModel>> Handle(GetTechnologyByIdRequest request, CancellationToken cancellationToken)
        {
            var technology = await this.dbContext.Technologies.FirstOrDefaultAsync(x => x.Id == request.Id);

            return technology is null
                ? Result<TechnologyResponseModel>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id))
                : Result<TechnologyResponseModel>.Success(new TechnologyResponseModel
                {
                    Name = technology.Name,
                    Description = technology.Description,
                    Id = request.Id,
                });
        }
    }
}
