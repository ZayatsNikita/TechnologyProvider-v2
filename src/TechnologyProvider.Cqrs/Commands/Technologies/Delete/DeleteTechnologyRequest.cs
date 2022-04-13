using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    /// <summary>
    /// Request to delete a technology.
    /// </summary>
    public class DeleteTechnologyRequest : IRequest<Result<object>>
    {
        /// <summary>
        /// Gets or sets technology id.
        /// </summary>
        public int Id { get; set; }
    }
}
