using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Categories.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x)
                .SetValidator(new CategoryModelValidator());
        }
    }
}
