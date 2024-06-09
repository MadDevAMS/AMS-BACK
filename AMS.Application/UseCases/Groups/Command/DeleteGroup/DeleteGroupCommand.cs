using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Group.Command.DeleteGroup
{
    public class DeleteGroupCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
