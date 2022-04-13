using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.TechnologyCategories.Core;

namespace TechnologyProvider.Cqrs.Queries.TechnologyCategories.GetAllByParams
{
    /// <summary>
    /// Request to get all technologies by category or(and) technology id.
    /// </summary>
    public class GetAllByParamsRequest : IRequest<Result<IEnumerable<TechnologyCategoriesResponseModel>>>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        [FromRoute]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets technologyId.
        /// </summary>
        [FromRoute]
        public int TechnologyId { get; set; }
    }
}
