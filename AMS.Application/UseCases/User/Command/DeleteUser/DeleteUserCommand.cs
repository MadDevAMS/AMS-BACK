using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.User.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
