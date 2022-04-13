using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    /// <summary>
    /// Request to update a category.
    /// </summary>
    public class UpdateCategoryRequest : IRequest<Result<object>>
    {
        /// <summary>
        /// Gets or sets category model information.
        /// </summary>
        [FromBody]
        public CategoryModel Category { get; set; }

        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        [FromRoute]
        public int Id { get; set; }
    }
}
