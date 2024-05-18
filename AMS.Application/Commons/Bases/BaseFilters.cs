namespace AMS.Application.Commons.Bases
{
    public class BaseFilters : BasePagination
    {
        public int? NumFilter { get; set; }
        public string? TextFilter { get; set; }
        public int? State { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
