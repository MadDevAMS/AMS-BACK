using AMS.Application.Dtos.Groups;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task CreateAsync(GroupsDto group);

        // ---------------------------------
        Task CreateWithUserAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(long groupId);

        // ---------------------------------

        Task<long> GroupExistsAsync(string groupName, long idEntidad);
    }
}