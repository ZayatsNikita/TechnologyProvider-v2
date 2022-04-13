using FluentValidation;

namespace TechnologyProvider.Cqrs.Commands.Categories.Core
{
    /// <summary>
    /// Validator for the category model.
    /// </summary>
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryModelValidator"/> class.
        /// </summary>
        public CategoryModelValidator()
        {
            this.RuleFor(x => x.Name).NotNull()
                .MinimumLength(ValidationConstants.MinNameLength)
                .MaximumLength(ValidationConstants.MaxNameLength);
        }
    }
}
