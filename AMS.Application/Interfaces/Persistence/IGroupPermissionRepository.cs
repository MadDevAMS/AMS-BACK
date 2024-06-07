using AMS.Application.Dtos.GroupPermission;
using AMS.Application.Dtos.Groups;
using AMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupPermissionRepository
    {
        Task CreateGroupPermissionsAsync(GroupPermission groupPermission);
    }
}
