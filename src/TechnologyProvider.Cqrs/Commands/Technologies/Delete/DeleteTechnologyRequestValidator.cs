using FluentValidation;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    /// <summary>
    /// Validator for the request to delete a technology.
    /// </summary>
    public class DeleteTechnologyRequestValidator : AbstractValidator<DeleteTechnologyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTechnologyRequestValidator"/> class.
        /// </summary>
        public DeleteTechnologyRequestValidator()
        {
            this.RuleFor(x => x.Id).MustBeValidForUseAsId();
        }
    }
}
