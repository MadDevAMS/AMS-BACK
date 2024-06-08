using AMS.Application.Commons.Bases;
using AMS.Domain.Entities;
using MediatR;

namespace AMS.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserCommnad : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int State { get; set; }
        public bool UpdateState { get; set; }
        public bool UpdatePassword { get; set; }
        public List<long> Groups { get; set; } = null!;
    }
}
