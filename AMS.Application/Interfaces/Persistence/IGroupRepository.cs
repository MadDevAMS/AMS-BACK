using AMS.Application.Dtos.Groups;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task DeleteAsync(long groupId);

    }
}
