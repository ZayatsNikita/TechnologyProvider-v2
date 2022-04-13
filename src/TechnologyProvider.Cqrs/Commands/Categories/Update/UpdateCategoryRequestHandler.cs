using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    /// <summary>
    /// Handler class for update a category.
    /// </summary>
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db Context.</param>
        public UpdateCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Just empty object.</returns>
        public async Task<Result<object>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var oldVersion = await this.dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (oldVersion == null)
            {
                return Result<object>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id));
            }

            oldVersion.Name = request.Category.Name;

            await this.dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }
    }
}
