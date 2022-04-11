using MediatR;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    public class CreateTechnologyRequest : TechnologyModel, IRequest<Result<CreateTechnologyResultModel>>
    {
    }
}
