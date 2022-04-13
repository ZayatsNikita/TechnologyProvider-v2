using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetById
{
    /// <summary>
    /// Request to get technology by id.
    /// </summary>
    public class GetTechnologyByIdRequest : IRequest<Result<TechnologyResponseModel>>
    {
        /// <summary>
        /// Gets or sets technology id.
        /// </summary>
        public int Id { get; set; }
    }
}
