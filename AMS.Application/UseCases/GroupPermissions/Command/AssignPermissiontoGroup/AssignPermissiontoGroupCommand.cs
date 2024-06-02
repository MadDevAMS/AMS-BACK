using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS.Application.Commons.Bases;

namespace AMS.Application.UseCases.GroupPermissions.Command.AssignPermissiontoGroup
{
    public class AssignPermissiontoGroupCommand : IRequest<BaseResponse<bool>>
    {
        public long GroupId { get; set; }
        public List<long> PermissionId { get; set; } = new List<long>();

    }
}
    