using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Update
{
    /// <summary>
    /// Validator for the request to delete a technology.
    /// </summary>
    public class UpdateTechnologyRequestValidator : AbstractValidator<UpdateTechnologyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTechnologyRequestValidator"/> class.
        /// </summary>
        public UpdateTechnologyRequestValidator()
        {
            this.RuleFor(x => x.Id)
                .MustBeValidForUseAsId();

            this.RuleFor(x => x.Technology)
                .SetValidator(new TechonologyModelValidator());
        }
    }
}
