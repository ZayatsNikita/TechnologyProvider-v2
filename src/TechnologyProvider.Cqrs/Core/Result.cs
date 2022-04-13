namespace TechnologyProvider.Cqrs.Core
{
    /// <summary>
    /// An object describing the result of the overgrowth.
    /// </summary>
    /// <typeparam name="T">The type of payload that the result of executing the request will contain.</typeparam>
    public class Result<T>
    {
        private readonly string? message;
        private readonly string? memberName;
        private readonly RequestStatus status;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="memberName">Name of the member that the error occurred with.</param>
        /// <param name="status">Result status.</param>
        protected Result(string message, string memberName, RequestStatus status)
        {
            this.message = message;
            this.memberName = memberName;
            this.status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="payload">Payload.</param>
        /// <param name="status">Result status.</param>
        protected Result(T payload, RequestStatus status)
        {
            this.Payload = payload;
            this.status = status;
        }

        /// <summary>
        /// Gets or sets payload.
        /// </summary>
        public T Payload { get; set; }

        /// <summary>
        /// Gets a value indicating whether has the request completed successfully.
        /// </summary>
        public bool IsSuccess => this.status == RequestStatus.Success;

        /// <summary>
        /// Gets a value indicating whether did the search result in a failed search.
        /// </summary>
        public bool IsNotFound => this.status == RequestStatus.NotFound;

        /// <summary>
        /// Gets a value indicating whether did the request end with a validation error.
        /// </summary>
        public bool IsValidationFailed => this.status == RequestStatus.ValidationFailed;

        /// <summary>
        /// Creating a successful result.
        /// </summary>
        /// <typeparam name="T">Payload type.</typeparam>
        /// <param name="payload">Payload.</param>
        /// <returns>Result object.</returns>
        public static Result<T> Success<T>(T payload) => new Result<T>(payload, RequestStatus.Success);

        /// <summary>
        /// Creating a NotFound result.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="memberName">Name of the member that the error occurred with.</param>
        /// <returns>Result object.</returns>
        public static Result<T> NotFound(string message, string memberName) => new Result<T>(message, memberName, RequestStatus.NotFound);

        /// <summary>
        /// Creating a ValidationFailed result.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="memberName">Name of the member that the error occurred with.</param>
        /// <returns>Result object.</returns>
        public static Result<T> ValidationFailed(string message, string memberName) => new Result<T>(message, memberName, RequestStatus.ValidationFailed);

        /// <summary>
        /// Method that creates a string describing the error.
        /// </summary>
        /// <returns>String describing the error.</returns>
        public string GetFailure() => string.Format("{0} \"{1}\": [ \"{2}\" ] {3}", "{", this.memberName, this.message, "}");
    }
}
