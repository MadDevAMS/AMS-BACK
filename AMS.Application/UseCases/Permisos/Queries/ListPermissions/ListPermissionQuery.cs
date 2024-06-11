using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using MediatR;

namespace AMS.Application.UseCases.Permisos.Queries.ListPermissions
{
    public class ListPermissionQuery : BasePagination, IRequest<PaginatorResponse<PermissionsListResponseDto>>
    {
    }
}