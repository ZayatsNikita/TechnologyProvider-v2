using FluentValidation;

namespace TechnologyProvider.Cqrs.Infrastructure.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, int> MustBeValidForUseAsId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(id => id > 0);
        }
    }
}
