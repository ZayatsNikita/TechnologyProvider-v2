namespace TechnologyProvider.Cqrs.Core
{
    /// <summary>
    /// Request execution statuses.
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// Request completed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// The requested resource was not found.
        /// </summary>
        NotFound,

        /// <summary>
        /// Validation error occurred.
        /// </summary>
        ValidationFailed,
    }
}
