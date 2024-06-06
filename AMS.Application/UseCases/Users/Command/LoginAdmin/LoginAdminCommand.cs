using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Users.Command.LoginAdmin
{
    public class LoginAdminCommand : IRequest<BaseResponse<string>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
