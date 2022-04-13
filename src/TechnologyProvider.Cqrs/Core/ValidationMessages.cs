namespace TechnologyProvider.Cqrs.Core
{
    /// <summary>
    /// A class that creates error messages.
    /// </summary>
    public static class ValidationMessages
    {
        /// <summary>
        /// The method that creates the NotFound error message.
        /// </summary>
        /// <param name="parameterName">Name of the parameter associated with the error.</param>
        /// <param name="parameterValue">The value of the parameter associated with the error.</param>
        /// <returns>Error message.</returns>
        public static string NotFoundMessage(string? parameterName, string? parameterValue)
            => $"The requested object with parameter name: {parameterName}, parameter value: {parameterValue} was not found.";

        /// <summary>
        /// The method that creates the validation error message.
        /// </summary>
        /// <param name="parameterName">Name of the parameter associated with the error.</param>
        /// <param name="parameterValue">The value of the parameter associated with the error.</param>
        /// <returns>Error message.</returns>
        public static string FailedValidationRulesMessage(string? parameterName, string? parameterValue)
            => $"The requested object with parameter name: {parameterName}, parameter value: {parameterValue} does not comply with the validation rules.";
    }
}
