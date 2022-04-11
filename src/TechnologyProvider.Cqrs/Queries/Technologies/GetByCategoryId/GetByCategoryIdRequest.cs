using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId
{
    public class GetByCategoryIdRequest : IRequest<Result<IEnumerable<TechnologyModel>>>
    {
        public  int Id { get; set; }
    }
}
