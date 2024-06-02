using AMS.Application.Dtos.GroupPermission;
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
        Task AssignPermissiontoGroupAsync(AddPermissionsToGroupDto addPermissionsToGroupDto);
    }
}
