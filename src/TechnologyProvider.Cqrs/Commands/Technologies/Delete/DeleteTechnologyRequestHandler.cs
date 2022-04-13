using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    /// <summary>
    /// Handler class for deleting a technology.
    /// </summary>
    public class DeleteTechnologyRequestHandler : IRequestHandler<DeleteTechnologyRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTechnologyRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db Context.</param>
        public DeleteTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Just empty object.</returns>
        public async Task<Result<object>> Handle(DeleteTechnologyRequest request, CancellationToken cancellationToken)
        {
            var technology = await this.dbContext.Set<Technology>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (technology == null)
            {
                return Result<object>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id));
            }

            this.dbContext.Technologies.Remove(technology);

            await this.dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }
    }
}
