using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task CreateAsync(CreateUserDto user, long userId);
        Task<long> UserExistAsync(string email);
        Task<UserDetailResponseDto> UserByEmailAsync(string email);
        Task<PaginatorResponse<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter, long idUser);
        Task DeleteAsync(long id, long userId);
        Task<long> IsUserAdminAsync(string email);
        Task<UserDetailResponseDto> UserByIdAsync(long id);
        Task UpdateAsync(CreateUserDto user, bool updateState, bool updatePassword, long userId);
        Task<AuthUserDto> AuthUserByIdAsync(long id);
    }
}
