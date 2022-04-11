using MediatR;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    public class UpdateTechnologyRequest : TechnologyModel, IRequest<Result<UpdateTechonologyResultModel>>
    {
        public int Id { get; set; }
    }
}
