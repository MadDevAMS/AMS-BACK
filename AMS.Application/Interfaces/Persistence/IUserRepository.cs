using AMS.Application.Dtos.User;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<long> UserExistAsync(string email);
        Task<UserDetailResponseDto> UserByEmailAsync(string email);
        Task<List<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter);
    }
}
