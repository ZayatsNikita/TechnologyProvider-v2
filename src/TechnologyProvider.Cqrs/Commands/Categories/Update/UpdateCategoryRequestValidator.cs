using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    /// <summary>
    /// Validator for the request to update a category.
    /// </summary>
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryRequestValidator"/> class.
        /// </summary>
        public UpdateCategoryRequestValidator()
        {
            this.RuleFor(x => x.Id)
                .MustBeValidForUseAsId();

            this.RuleFor(x => x.Category)
                .SetValidator(new CategoryModelValidator());
        }
    }
}
