using AMS.Application.Commons.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Dtos.Filters
{
    public class ListPermissionFilter : BasePagination
    {
        public long IdPermission { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public int PermissionState { get; set; }
    }
}
