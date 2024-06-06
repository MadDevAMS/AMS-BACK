using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task UpdateAsync(GroupUpdateDto group);
        Task CreateAsync(GroupCreateDto groupDto);
        Task<GroupByIdDto> GetGroupByIdAsync(long groupId);
        Task<PaginatorResponse<GroupListDto>> ListGroups(ListGroupFilter filter);
    }
}
