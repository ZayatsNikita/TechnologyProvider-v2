using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetAll
{
    public class GetAllRequest : IRequest<Result<IEnumerable<TechnologyModel>>>
    {
    }
}
