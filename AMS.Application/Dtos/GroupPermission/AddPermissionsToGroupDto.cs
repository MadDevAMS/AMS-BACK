using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Dtos.GroupPermission
{
    public class AddPermissionsToGroupDto
    {
        public long GroupId { get; set; }
        public long GroupPermissionId { get; set; }
        public List<long> PermissionId { get; set; } = new List<long>();
    }
}
