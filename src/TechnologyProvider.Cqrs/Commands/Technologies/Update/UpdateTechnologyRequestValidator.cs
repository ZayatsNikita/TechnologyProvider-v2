using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    public class UpdateTechnologyRequestValidator : AbstractValidator<UpdateTechnologyRequest>
    {
        public UpdateTechnologyRequestValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidForUseAsId();

            RuleFor(x => x)
                .SetValidator(new TechonologyModelValidator());
        }
    }
}
