using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IPermissionRepository
    {
        Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionQuery filter);
    }
}