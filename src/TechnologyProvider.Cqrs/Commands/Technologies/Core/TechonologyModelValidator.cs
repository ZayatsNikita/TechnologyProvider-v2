using FluentValidation;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Core
{
    /// <summary>
    /// Validator for TechnologyModel.
    /// </summary>
    public class TechonologyModelValidator : AbstractValidator<TechnologyModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechonologyModelValidator"/> class.
        /// </summary>
        public TechonologyModelValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(ValidationConstants.MinNameLength)
                .MaximumLength(ValidationConstants.MaxNameLength);

            this.RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.MaxDescriptionLength);
        }
    }
}
