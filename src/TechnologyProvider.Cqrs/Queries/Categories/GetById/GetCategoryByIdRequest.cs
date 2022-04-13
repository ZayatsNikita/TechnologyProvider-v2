using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetById
{
    /// <summary>
    /// Request to get a category by id.
    /// </summary>
    public class GetCategoryByIdRequest : IRequest<Result<CategoryResponseModel>>
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int Id { get; set; }
    }
}
