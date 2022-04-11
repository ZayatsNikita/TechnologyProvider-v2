using FluentValidation;

namespace TechnologyProvider.Cqrs.Commands.Categories.Core
{
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .MinimumLength(ValidationConstants.MinNameLength)
                .MaximumLength(ValidationConstants.MaxNameLength);
        }
    }
}
