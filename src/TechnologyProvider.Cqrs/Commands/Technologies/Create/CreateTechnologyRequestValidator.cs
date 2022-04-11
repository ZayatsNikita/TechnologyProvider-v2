using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    public class CreateTechnologyRequestValidator : AbstractValidator<CreateTechnologyRequest>
    {
        public CreateTechnologyRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x)
                .SetValidator(new TechonologyModelValidator());
        }
    }
}
