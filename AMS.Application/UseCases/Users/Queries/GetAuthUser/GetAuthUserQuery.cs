using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.User;
using MediatR;

namespace AMS.Application.UseCases.Users.Queries.GetAuthUser
{
    public class GetAuthUserQuery: IRequest<BaseResponse<AuthUserDto>>
    {
    }
}
