using MediatR;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    public class DeleteTechnologyRequest : IRequest<Result<object>>
    {
        public int Id { get; set; }
    }
}
