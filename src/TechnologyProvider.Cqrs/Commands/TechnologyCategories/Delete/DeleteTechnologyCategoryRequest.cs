using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Delete
{
    /// <summary>
    /// Request to delete technology-category model.
    /// </summary>
    public class DeleteTechnologyCategoryRequest : IRequest<Result<object>>
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
