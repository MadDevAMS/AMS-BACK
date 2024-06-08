using AMS.Application.Commons.Bases;
using AMS.Domain.Entities;
using MediatR;

namespace AMS.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponse<bool>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public List<long> Groups { get; set; } = null!;
    }
}
