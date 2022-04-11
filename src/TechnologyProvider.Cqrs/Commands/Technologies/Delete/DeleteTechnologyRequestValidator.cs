using FluentValidation;
using TechnologyProvider.Cqrs.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Delete
{
    public class DeleteTechnologyRequestValidator : AbstractValidator<DeleteTechnologyRequest>
    {
        public DeleteTechnologyRequestValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidForUseAsId();
        }
    }
}
