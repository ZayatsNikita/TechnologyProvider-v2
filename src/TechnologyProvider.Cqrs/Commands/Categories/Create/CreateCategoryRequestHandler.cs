using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    /// <summary>
    /// The handler responsible for creating the category.
    /// </summary>
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Result<CreateCategoryRequestResult>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context object.</param>
        public CreateCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Id of the created object.</returns>
        public async Task<Result<CreateCategoryRequestResult>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            this.dbContext.Categories.Add(category);

            await this.dbContext.SaveChangesAsync();

            return Result<CreateCategoryRequestResult>.Success(new CreateCategoryRequestResult
            {
                Id = category.Id,
                Name = request.Name,
            });
        }
    }
}
