using AMS.Application.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.GroupUsers.Command.CreateGroupUsers
{
    public class CreateGroupUsersCommand : IRequest<BaseResponse<bool>>
    {
        public long GroupId { get; set; }
        public long UserId { get; set; }
    }
}
