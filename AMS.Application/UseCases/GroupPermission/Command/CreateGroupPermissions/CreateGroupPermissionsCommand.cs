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
        public List<long> GroupId { get; set; } = new List<long>();
        public List<long> PermissionId { get; set; } = new List<long>();

    }
}
