using AMS.Application.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.UseCases.Groups.Command.GetGroups
{
    public class GetGroupByIdCommand : IRequest<BaseResponse<bool>>
    {
        public long GroupId { get; set; }

    }
}
