using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    /// <summary>
    /// Validator for the request to create a technology.
    /// </summary>
    public class CreateTechnologyRequestValidator : AbstractValidator<CreateTechnologyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnologyRequestValidator"/> class.
        /// </summary>
        public CreateTechnologyRequestValidator()
        {
            this.RuleFor(x => x)
                .NotNull();

            this.RuleFor(x => x)
                .SetValidator(new TechonologyModelValidator());
        }
    }
}
