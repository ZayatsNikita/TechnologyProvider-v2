using FluentValidation;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Core
{
    public class TechonologyModelValidator : AbstractValidator<TechnologyModel>
    {
        public TechonologyModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(ValidationConstants.MinNameLength)
                .MaximumLength(ValidationConstants.MaxNameLength);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.MaxDescriptionLength);

            RuleFor(x => x.CategoryIds)
                .NotEmpty();

            RuleForEach(x => x.CategoryIds).MustBeValidForUseAsId();

            RuleForEach(x => x.CategoryIds).GreaterThanOrEqualTo(ValidationConstants.MinCategoryId);
        }
    }
}
