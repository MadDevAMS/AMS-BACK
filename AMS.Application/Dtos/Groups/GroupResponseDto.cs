using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Dtos.Groups
{
    public class GroupResponseDto
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string GroupDescription { get; set; } = null!;
        public List<long> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
        public string PermissionName { get; set; } = null!;
        public int StatePermission { get; set; }

    }
}
