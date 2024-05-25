using MediatR;

namespace AMS.Application.Commons.Bases
{
    public class BaseResponse<T> : IRequest
    {
        public int Status { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = null!;
        public int TotalRecords { get; set; }
        public IEnumerable<BaseError>? Errors { get; set; }
    }
}
