using FluentValidation;

namespace TechnologyProvider.Cqrs.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for validation ruls.
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// A method that checks the suitability of the value for use as an id.
        /// </summary>
        /// <typeparam name="T">request type.</typeparam>
        /// <param name="ruleBuilder">Rule builder.</param>
        /// <returns>Valiadtion result.</returns>
        public static IRuleBuilderOptions<T, int> MustBeValidForUseAsId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(id => id > 0);
        }
    }
}
