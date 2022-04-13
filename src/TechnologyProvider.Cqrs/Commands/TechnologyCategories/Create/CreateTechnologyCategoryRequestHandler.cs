using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Create
{
    /// <summary>
    /// The handler responsible for creating the category-technology pair.
    /// </summary>
    public class CreateTechnologyCategoryRequestHandler : IRequestHandler<CreateTechnologyCategoryRequest, Result<int>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnologyCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context object.</param>
        public CreateTechnologyCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Id of the created object.</returns>
        public async Task<Result<int>> Handle(CreateTechnologyCategoryRequest request, CancellationToken cancellationToken)
        {
            if (!this.IsCategoryExsist(request.CategoryId))
            {
                return Result<int>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.CategoryId), request.CategoryId.ToString()), nameof(request.CategoryId));
            }

            if (!this.IsTechnologyExsist(request.TechnologyId))
            {
                return Result<int>.NotFound(ValidationMessages.NotFoundMessage(nameof(request.TechnologyId), request.TechnologyId.ToString()), nameof(request.TechnologyId));
            }

            if (this.IsCombinationOfIdsExsist(request.CategoryId, request.TechnologyId))
            {
                return Result<int>.ValidationFailed(ValidationMessages.FailedValidationRulesMessage("TechnologyId & CategoryId", $"{request.TechnologyId} & {request.CategoryId}"), "TechnologyId & CategoryId");
            }

            var technologyCategory = new TechnologyCategory
            {
                TechnologyId = request.TechnologyId,
                CategoryId = request.CategoryId,
            };

            this.dbContext.TechnologyCategories.Add(technologyCategory);

            await this.dbContext.SaveChangesAsync();

            return Result<int>.Success(5);
        }

        private bool IsTechnologyExsist(int technologyId)
        {
            return this.dbContext.Technologies.Any(x => x.Id == technologyId);
        }

        private bool IsCategoryExsist(int categoryId)
        {
            return this.dbContext.Categories.Any(x => x.Id == categoryId);
        }

        private bool IsCombinationOfIdsExsist(int categoryId, int technologyId)
        {
            return this.dbContext.TechnologyCategories.Any(x => x.CategoryId == categoryId && x.TechnologyId == technologyId);
        }
    }
}
