using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.User.Command.Login
{
    public class LoginCommand : IRequest<BaseResponse<string>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
