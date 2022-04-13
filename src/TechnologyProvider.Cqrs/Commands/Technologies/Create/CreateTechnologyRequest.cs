using MediatR;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    /// <summary>
    /// Technology Creation Request.
    /// </summary>
    public class CreateTechnologyRequest : TechnologyModel, IRequest<Result<CreateTechnologyRequestResult>>
    {
    }
}
