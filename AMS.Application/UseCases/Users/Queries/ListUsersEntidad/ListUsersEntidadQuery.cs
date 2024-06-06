using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.User;
using MediatR;

namespace AMS.Application.UseCases.User.Queries.ListUsersEntidad
{
    public class ListUsersEntidadQuery : BasePagination, IRequest<PaginatorResponse<ListUsersResponseDto>>
    {
        public long IdEntidad { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public int State { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
