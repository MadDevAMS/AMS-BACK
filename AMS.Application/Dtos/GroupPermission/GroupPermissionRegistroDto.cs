using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Dtos.GroupPermission
{
    public class GroupPermissionRegistroDto
    {
        public string GroupName { get; set; } = null!;

        public List<long> GroupId { get; set; } = null!;
        public List<long> PermissionId { get; set; } = null!;
    }
}
