using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetAll
{
    /// <summary>
    /// Request to get all categories.
    /// </summary>
    public class GetAllRequest : IRequest<Result<IEnumerable<CategoryResponseModel>>>
    {
    }
}
