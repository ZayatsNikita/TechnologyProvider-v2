using System.Linq;
using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Delete
{
    /// <summary>
    /// The handler responsible for deleting the category-technology pair.
    /// </summary>
    public class DeleteTechnologyCategoryRequestHandler : IRequestHandler<DeleteTechnologyCategoryRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTechnologyCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context object.</param>
        public DeleteTechnologyCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Id of the created object.</returns>
        public async Task<Result<object>> Handle(DeleteTechnologyCategoryRequest request, CancellationToken cancellationToken)
        {
            var technologyCategoryPair = this.dbContext.TechnologyCategories
                .FirstOrDefault(x => x.CategoryId == request.CategoryId && x.TechnologyId == request.TechnologyId);

            if (technologyCategoryPair is null)
            {
                return Result<object>.NotFound(ValidationMessages.NotFoundMessage("TechnologyId & CategoryId", $"{request.TechnologyId} & {request.CategoryId}"), "TechnologyId & CategoryId");
            }
            else
            {
                this.dbContext.TechnologyCategories.Remove(technologyCategoryPair);

                await this.dbContext.SaveChangesAsync();

                return Result<object>.Success(new object());
            }
        }
    }
}
