using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Roles;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IPermissionRepository
    {
        Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionFilter filter);
    }
}
