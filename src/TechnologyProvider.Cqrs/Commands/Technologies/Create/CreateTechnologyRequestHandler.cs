using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    /// <summary>
    /// Technology Creation Request Handler.
    /// </summary>
    public class CreateTechnologyRequestHandler : IRequestHandler<CreateTechnologyRequest, Result<CreateTechnologyRequestResult>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnologyRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db Context.</param>
        public CreateTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Request processing method.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><see cref="Result"/> object containing an id or error information.</returns>
        public async Task<Result<CreateTechnologyRequestResult>> Handle(CreateTechnologyRequest request, CancellationToken cancellationToken)
        {
            if (await this.IsThereSameNameInTheStorage(request))
            {
                Result<CreateTechnologyRequestResult>.ValidationFailed(ValidationMessages.FailedValidationRulesMessage(nameof(request.Name), request.Name), nameof(request.Name));
            }

            var technology = new Technology
            {
                Description = request.Description,
                Name = request.Name,
            };

            this.dbContext.Technologies.Add(technology);

            await this.dbContext.SaveChangesAsync();

            return Result<CreateTechnologyRequestResult>.Success(new CreateTechnologyRequestResult
            {
                Id = technology.Id,
                Name = technology.Name,
                Description = technology.Description
            });
        }

        private async Task<bool> IsThereSameNameInTheStorage(CreateTechnologyRequest request)
        {
            return await this.dbContext.Set<Technology>().AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());
        }
    }
}
