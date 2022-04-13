using FluentValidation;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Categories.Delete
{
    /// <summary>
    /// Validator for the request to delete a category.
    /// </summary>
    public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCategoryRequestValidator"/> class.
        /// </summary>
        public DeleteCategoryRequestValidator()
        {
            this.RuleFor(x => x.Id)
                .MustBeValidForUseAsId();
        }
    }
}
