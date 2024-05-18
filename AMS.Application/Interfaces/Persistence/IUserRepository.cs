using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<long> UserExistAsync(string email);
        Task UpdateAsync(User user, bool updateState);
    }
}
