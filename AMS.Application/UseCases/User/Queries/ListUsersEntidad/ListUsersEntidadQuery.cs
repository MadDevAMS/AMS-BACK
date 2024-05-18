using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.User;
using MediatR;

namespace AMS.Application.UseCases.User.Queries.ListUsersEntidad
{
    public class ListUsersEntidadQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<ListUsersResponseDto>>>
    {
        public long IdEntidad { get; set; }
    }
}
