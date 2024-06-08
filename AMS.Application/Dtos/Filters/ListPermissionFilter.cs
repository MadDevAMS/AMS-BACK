using AMS.Application.Commons.Bases;

namespace AMS.Application.Dtos.Filters
{
    public class ListPermissionFilter : BasePagination
    {
        public long IdPermission { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public int PermissionState { get; set; }
    }
}