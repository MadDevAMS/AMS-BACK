using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.Permissions;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task UpdateAsync(GroupsDto group, long userId);
        Task CreateAsync(GroupsDto groupDto, long userId);
        Task<GroupByIdDto> GetGroupByIdAsync(long groupId);
        Task DeleteAsync(long id, long idUser);
        Task<PaginatorResponse<GroupListDto>> ListGroups(ListGroupFilter filter);
        Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionQuery filter);
    }
}
