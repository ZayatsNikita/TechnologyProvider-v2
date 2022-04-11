namespace TechnologyProvider.Cqrs.Core
{
    public class Result<T>
    {
        private readonly string? _message;
        private readonly string? _memberName;
        private readonly RequestStatus _status;

        protected Result(string message, string memberName, RequestStatus status)
        {
            _message = message;
            _memberName = memberName;
            _status = status;
        }

        protected Result(T payload, RequestStatus status)
        {
            Payload = payload;
            _status = status;
        }

        public T Payload { get;set; }

        public bool IsSuccess => _status == RequestStatus.Success;

        public bool IsNotFound => _status == RequestStatus.NotFound;

        public bool IsValidationFailed => _status == RequestStatus.ValidationFailed;

        public static Result<T> Success<T>(T payload) => new Result<T>(payload, RequestStatus.Success);

        public static Result<T> NotFound(string message, string memberName) => new Result<T>(message, memberName, RequestStatus.NotFound);

        public static Result<T> ValidationFailed(string message, string memberName) => new Result<T>(message, memberName, RequestStatus.ValidationFailed);


        public string GetFailure() => string.Format("{0} \"{1}\": [ \"{2}\" ] {3}", "{", _memberName, _message, "}");
    }
}
