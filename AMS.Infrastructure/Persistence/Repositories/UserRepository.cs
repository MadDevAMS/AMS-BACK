using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository(context), IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            user.State = Utils.ESTADO_ACTIVO;
            user.AuditCreateDate = DateTime.Now;
            user.AuditCreateUser = Utils.ESTADO_ACTIVO;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatorResponse<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter)
        {
            var query = _context.Users.Where(u => u.IdEntidad == filter.IdEntidad
                    && (u.FirstName.Contains(filter.UserName) || filter.UserName == Utils.EMPTY_STRING)
                    && (u.Email.Contains(filter.UserEmail) || filter.UserEmail == Utils.EMPTY_STRING)
                    && (u.State == filter.State || filter.State == -1)
                    && (u.AuditDeleteUser == null && u.AuditDeleteDate == null));

            var totalRecords = await query.Select(u => u.Id).CountAsync();

            var users = await query.Select(u => new ListUsersResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                State = u.State,
                AuditCreateDate = u.AuditCreateDate,
                Group = u.GroupUsers.Select(g => new GroupListResponseDto
                {
                    Id = g.GroupId,
                    Name = g.Group.Name,
                }).ToList()
            })
            .OrderBy(u => u.FirstName)
            .Skip((filter.NumPage - 1) * filter.Records)
            .Take(filter.Records)
            .ToListAsync();

            var result = new PaginatorResponse<ListUsersResponseDto>
            {
                Data = users,
                TotalRecords = totalRecords,
                CurrentPage = filter.NumPage,
                PageSize = filter.Records,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.Records)
            };

            return result;
        }

        public async Task UpdateAsync(User user, bool updateState)
        {
            var entity = (await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync())!;

            if (updateState)
            {
                entity.State = user.State;
            }
            else
            {
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<long> UserExistAsync(string email)
        {
            return await _context.Users.Where(u => u.Email.Equals(email))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDetailResponseDto> UserByEmailAsync(string email)
        {
            var userDetails = await _context.Users
                .AsNoTracking()
                .Where(u => u.Email.Equals(email) && u.State == 1 && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                .Select(u => new UserDetailResponseDto()
                {
                    UserId = u.Id,
                    Name = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IdEntidad = u.IdEntidad,
                    Password = u.Password,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Select(gu => gu.Group.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }

        public async Task DeleteAsync(long id, long userId)
        {
            var entity = (await _context.Users.FirstOrDefaultAsync(u => u.Id == id))!;

            entity.AuditDeleteUser = userId;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<long> IsUserAdminAsync(string email)
        {
            return await _context.GroupUsers.Where(gu => gu.User.Email == email
                    && gu.GroupId == Utils.GROUP_ADMIN_ID)
                .Select(u => u.UserId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDetailResponseDto> UserByIdAsync(long id)
        {
            var userDetails = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id && u.State == 1 && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                .Select(u => new UserDetailResponseDto()
                {
                    Name = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IdEntidad = u.IdEntidad,
                    Password = u.Password,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Select(gu => gu.Group.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }
    }
}
