using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task CreateAsync(CreateUserDto user);
        Task<long> UserExistAsync(string email);
        Task<UserDetailResponseDto> UserByEmailAsync(string email);
        Task<PaginatorResponse<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter);
        Task DeleteAsync(long id, long userId);
        Task<long> IsUserAdminAsync(string email);
        Task<UserDetailResponseDto> UserByIdAsync(long id);
        Task UpdateAsync(CreateUserDto user, bool updateState, bool updatePassword);
        Task<AuthUserDto> AuthUserByIdAsync(long idUser);
    }
}
