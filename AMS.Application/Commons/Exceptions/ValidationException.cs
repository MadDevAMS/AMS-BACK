using AMS.Application.Commons.Bases;

namespace AMS.Application.Commons.Exceptions
{
    public class ValidationException() : Exception
    {
        public IEnumerable<BaseError>? Errors { get; } = new List<BaseError>();

        public ValidationException(IEnumerable<BaseError> errors) : this()
        {
            Errors = errors;
        }
    }
}
