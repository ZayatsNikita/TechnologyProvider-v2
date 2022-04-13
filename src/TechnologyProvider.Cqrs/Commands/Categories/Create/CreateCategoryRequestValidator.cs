using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Categories.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    /// <summary>
    /// Validator for the request to create a category.
    /// </summary>
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryRequestValidator"/> class.
        /// </summary>
        public CreateCategoryRequestValidator()
        {
            this.RuleFor(x => x)
                .SetValidator(new CategoryModelValidator());
        }
    }
}
