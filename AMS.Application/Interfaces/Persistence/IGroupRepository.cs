using AMS.Application.Dtos.Groups;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task UpdateAsync(GroupsDto group);
    }
}
