using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Categories.Delete
{
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public DeleteCategoryRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<object>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category == null)
            {
                return Result<object>.NotFound("", nameof(request.Id));
            }

            if (_dbContext.TechnologyCategories.Any(x => x.CategoryId == category.Id))
            {
                return Result<object>.ValidationFailed("",nameof(category.Id));
            }

            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }
    }
}
