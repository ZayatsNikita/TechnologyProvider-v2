using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;

namespace TechnologyProvider.Cqrs.Queries.Technologies.GetById
{
    public class GetByIdRequest : IRequest<Result<TechnologyModel>>
    {
        public  int Id { get; set; }
    }
}
