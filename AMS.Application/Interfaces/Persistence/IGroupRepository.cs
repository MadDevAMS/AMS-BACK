using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task UpdateAsync(GroupsDto group);
        Task CreateAsync(GroupsDto groupDto);
        Task<GroupByIdDto> GetGroupByIdAsync(long groupId);
        Task DeleteAsync(long id, long idUser);
        Task<PaginatorResponse<GroupListDto>> ListGroups(ListGroupFilter filter);
    }
}
