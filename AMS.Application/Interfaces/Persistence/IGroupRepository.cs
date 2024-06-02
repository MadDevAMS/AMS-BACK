namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupRepository
    {
        Task DeleteAsync(long groupId);

    }
}
