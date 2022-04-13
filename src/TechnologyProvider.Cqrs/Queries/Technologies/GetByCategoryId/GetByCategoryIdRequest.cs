using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId
{
    /// <summary>
    /// Request to get a collection of technologies related to a specific category.
    /// </summary>
    public class GetByCategoryIdRequest : IRequest<Result<IEnumerable<TechnologyResponseModel>>>
    {
        /// <summary>
        /// Gets or sets category Code.
        /// </summary>
        public int Id { get; set; }
    }
}
