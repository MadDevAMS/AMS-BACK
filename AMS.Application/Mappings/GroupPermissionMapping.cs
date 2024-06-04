using AMS.Application.Dtos.GroupPermission;
using AMS.Application.UseCases.GroupPermission.Command.CreateGroupPermissions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Mappings
{
    public class GroupPermissionMapping : Profile
    {
        public GroupPermissionMapping() {

            CreateMap< CreateGroupPermissionsCommand ,GroupPermissionRegistroDto >();
        
        }
        
    }
}
