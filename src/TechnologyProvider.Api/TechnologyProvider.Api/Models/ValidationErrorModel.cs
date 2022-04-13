namespace TechnologyProvider.Api.Models
{
    /// <summary>
    /// Model to send information about exception to user.
    /// </summary>
    public class ValidationErrorModel
    {
        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets name of the fild with error.
        /// </summary>
        public string? FieldName { get; set; }
    }
}
