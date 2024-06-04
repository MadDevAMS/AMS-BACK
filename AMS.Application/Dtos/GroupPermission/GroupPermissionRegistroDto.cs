using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Dtos.GroupPermission
{
    public class GroupPermissionRegistroDto
    {
        public List<long> GroupId { get; set; } = new List<long>();   
        public List<long> PermissionId { get; set; } = new List<long>();
    }
}
