using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.TechnologyCategories.Core;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.Cqrs.Queries.TechnologyCategories.GetAllByParams
{
    /// <summary>
    /// The handler responsible for getting TechnologyCategory pairs.
    /// </summary>
    public class GetAllTechnologyCategoriesByParamsRequestHandler : IRequestHandler<GetAllByParamsRequest, Result<IEnumerable<TechnologyCategoriesResponseModel>>>
    {
        private readonly TechnologyProviderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTechnologyCategoriesByParamsRequestHandler"/> class.
        /// </summary>
        /// <param name="dbContext">Db context object.</param>
        public GetAllTechnologyCategoriesByParamsRequestHandler(TechnologyProviderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// The method that processes the request.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response object wuth collection of <see cref="IEnumerable{T}"/> or error.</returns>
        public async Task<Result<IEnumerable<TechnologyCategoriesResponseModel>>> Handle(GetAllByParamsRequest request, CancellationToken cancellationToken)
        {
            var collectionForResponse = this.dbContext.TechnologyCategories.AsNoTracking();

            if (request.CategoryId > 0)
            {
                collectionForResponse = collectionForResponse.Where(c => c.CategoryId == request.CategoryId);
            }

            if (request.TechnologyId > 0)
            {
                collectionForResponse = collectionForResponse.Where(c => c.TechnologyId == request.TechnologyId);
            }

            var response = await collectionForResponse.Select(x => new TechnologyCategoriesResponseModel
            {
                CategoryId = x.CategoryId,
                TechnologyId = x.TechnologyId,
            }).ToArrayAsync();

            return Result<IEnumerable<TechnologyCategoriesResponseModel>>.Success(response.AsEnumerable());
        }
    }
}
