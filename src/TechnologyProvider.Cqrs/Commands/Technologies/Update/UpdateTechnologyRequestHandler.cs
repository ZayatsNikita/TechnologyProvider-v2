using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    /// <summary>
    /// Handler class for updating a technology.
    /// </summary>
    public class UpdateTechnologyRequestHandler : IRequestHandler<UpdateTechnologyRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTechnologyRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db Context.</param>
        public UpdateTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Just empty object.</returns>
        public async Task<Result<object>> Handle(UpdateTechnologyRequest request, CancellationToken cancellationToken)
        {
            var oldVersion = await this.dbContext.Set<Technology>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (oldVersion == null)
            {
                return Result<object>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id));
            }

            if (await this.IsThereSameNameInTheStorage(request, oldVersion))
            {
                Result<object>.ValidationFailed(ValidationMessages.FailedValidationRulesMessage(nameof(request.Technology.Name), request.Technology.Name), nameof(request.Technology.Name));
            }

            oldVersion.Name = request.Technology.Name;
            oldVersion.Description = request.Technology.Description;

            await this.dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }

        private async Task<bool> IsThereSameNameInTheStorage(UpdateTechnologyRequest request, Technology oldVersion)
        {
            if (oldVersion.Name == request.Technology.Name)
            {
                return false;
            }
            else
            {
                return await this.dbContext.Technologies.AnyAsync(x => x.Name.ToLower() == request.Technology.Name.ToLower());
            }
        }
    }
}
