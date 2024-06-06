using AMS.Application.Commons.Bases;

namespace AMS.Application.Dtos.Filters
{
    public class ListGroupFilter: BasePagination
    {
        public long IdEntidad { get; set; }
    }
}
