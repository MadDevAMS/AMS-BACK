using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Roles;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IPermissionRepository
    {
        Task<long> PermissionExistAsync(string PermissionName);
        Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionQuery filter);
    }
}
