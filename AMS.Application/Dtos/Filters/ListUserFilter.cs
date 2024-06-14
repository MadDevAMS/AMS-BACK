using AMS.Application.Commons.Bases;

namespace AMS.Application.Dtos.Filters
{
    public class ListUserFilter : BasePagination
    {
        public long IdUserQuery {  get; set; }
        public long IdEntidad { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public int State { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
