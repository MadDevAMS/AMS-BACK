using AMS.Application.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupPermission.Command.CreateGroupPermissions
{
    public class CreateGroupPermissionsCommand : IRequest<BaseResponse<bool>>
    {
        public long GroupId { get; set; }
        public long PermissionId { get; set; }
        public int State { get; set; }

    }
}
