using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommnad : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int State {  get; set; }
        public bool UpdateState { get; set; }
    }
}
