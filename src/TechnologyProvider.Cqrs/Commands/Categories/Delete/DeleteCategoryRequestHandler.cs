using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Commands.Categories.Delete
{
    /// <summary>
    /// Handler class for deleting a category.
    /// </summary>
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db Context.</param>
        public DeleteCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Just empty object.</returns>
        public async Task<Result<object>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category == null)
            {
                return Result<object>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.Id), request.Id.ToString()), nameof(request.Id));
            }

            this.dbContext.Categories.Remove(category);

            await this.dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }
    }
}
