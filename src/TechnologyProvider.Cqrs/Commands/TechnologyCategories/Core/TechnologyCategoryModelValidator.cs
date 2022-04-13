using FluentValidation;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Core
{
    /// <summary>
    /// Validator for the technology-category pairs model.
    /// </summary>
    public class TechnologyCategoryModelValidator : AbstractValidator<TechnologyCategoryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnologyCategoryModelValidator"/> class.
        /// </summary>
        public TechnologyCategoryModelValidator()
        {
            this.RuleFor(x => x.CategoryId)
                .MustBeValidForUseAsId();

            this.RuleFor(x => x.TechnologyId)
                .MustBeValidForUseAsId();
        }
    }
}
