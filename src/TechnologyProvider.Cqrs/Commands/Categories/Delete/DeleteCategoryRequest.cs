using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Delete
{
    /// <summary>
    /// Request to delete a category.
    /// </summary>
    public class DeleteCategoryRequest : IRequest<Result<object>>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int Id { get; set; }
    }
}
