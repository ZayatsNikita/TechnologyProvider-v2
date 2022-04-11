using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.DataAccess.Models;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    public class DeleteTechnologyRequestHandler : IRequestHandler<DeleteTechnologyRequest, Result<object>>
    {
        private readonly TechnologyProviderDbContext _dbContext;

        public DeleteTechnologyRequestHandler(TechnologyProviderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<object>> Handle(DeleteTechnologyRequest request, CancellationToken cancellationToken)
        {
            var technology = await _dbContext.Set<Technology>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (technology == null)
            {
                return Result<object>.NotFound("", nameof(request.Id));
            }

            _dbContext.Technologies.Remove(technology);

            await _dbContext.SaveChangesAsync();

            return Result<object>.Success(new object());
        }
    }
}
