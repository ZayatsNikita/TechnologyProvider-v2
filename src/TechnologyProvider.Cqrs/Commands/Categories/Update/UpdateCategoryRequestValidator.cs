using FluentValidation;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidForUseAsId();

            RuleFor(x => x)
                .SetValidator(new CategoryModelValidator());
        }
    }
}
