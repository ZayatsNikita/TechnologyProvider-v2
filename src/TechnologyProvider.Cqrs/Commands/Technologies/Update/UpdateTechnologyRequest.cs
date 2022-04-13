using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    /// <summary>
    /// Request to update a category.
    /// </summary>
    public class UpdateTechnologyRequest : IRequest<Result<object>>
    {
        /// <summary>
        /// Gets or sets technology model information.
        /// </summary>
        [FromBody]
        public TechnologyModel Technology { get; set; }

        /// <summary>
        /// Gets or sets technology id.
        /// </summary>
        public int Id { get; set; }
    }
}
