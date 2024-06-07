using AMS.Application.Dtos.Groups;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task<long> GroupExistAsync(string GroupName);
        Task UpdateAsync(GroupsDto group);
    }
}
