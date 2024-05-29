using MediatR;

namespace AMS.Application.Commons.Bases
{
    public class PaginatorResponse<T> : IRequest
    {
        public int Status { get; set; }
        public List<T>? Data { get; set; }
        public string Message { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
