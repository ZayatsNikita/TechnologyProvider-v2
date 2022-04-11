using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetAll
{
    public class GetAllRequest : IRequest<Result<IEnumerable<CategoryModel>>>
    {
    }
}
