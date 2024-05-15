using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponse<bool>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdEntidad { get; set; }
    }
}
