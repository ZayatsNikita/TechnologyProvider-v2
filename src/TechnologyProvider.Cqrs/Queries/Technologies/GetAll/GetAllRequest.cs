using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetAll
{
    /// <summary>
    /// Request for all technologies.
    /// </summary>
    public class GetAllRequest : IRequest<Result<IEnumerable<TechnologyResponseModel>>>
    {
    }
}
