using AMS.Application.Dtos.User;
using System.Linq.Dynamic.Core;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.Roles;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task CreateAsync(User user)
        {
            user.State = Utils.ESTADO_ACTIVO;
            user.AuditCreateDate = DateTime.Now;
            user.AuditCreateUser = Utils.ESTADO_ACTIVO;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.IdEntidad == filter.IdEntidad);

            if (!string.IsNullOrEmpty(filter.TextFilter) && filter.NumFilter is not null)
            {
                switch (filter.NumFilter)
                {
                    case 1:
                        query = query.Where(u => u.FirstName.Contains(filter.TextFilter, StringComparison.OrdinalIgnoreCase));
                        break;
                    case 2:
                        query = query.Where(u => u.LastName.Contains(filter.TextFilter, StringComparison.OrdinalIgnoreCase));
                        break;
                    case 3:
                        query = query.Where(u => u.Email.Contains(filter.TextFilter, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }

            if (filter.State is not null)
            {
                query = query.Where(u => u.State == filter.State);
            }

            if (!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
            {
                var startDate = Convert.ToDateTime(filter.StartDate).ToUniversalTime();
                var endDate = Convert.ToDateTime(filter.EndDate).ToUniversalTime().AddDays(1);

                query = query.Where(x => x.AuditCreateDate >= startDate && x.AuditCreateDate <= endDate);
            }

            query = filter.Order == "desc"
                ? query.OrderBy($"{filter.Sort} descending")
                : query.OrderBy($"{filter.Sort} ascending");
            query = query.Skip((filter.NumPage - 1) * filter.Records).Take(filter.Records);


            var result = await query
            .Select(u => new ListUsersResponseDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                State = u.State,
                Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Select(rp => new PermissionsListResponseDto
                        {
                            Id = rp.PermissionId,
                            Name = rp.Permission.Name
                        })
                        .ToList(),
                Group = u.GroupUsers.Select(g => new GroupListResponseDto
                {
                    Id = g.Groupid,
                    Name = g.Group.Name,
                }).ToList()
            }).ToListAsync();

            return result;
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
                    Name = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    IdEntidad = u.IdEntidad,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Select(gu => gu.Group.Description)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }
    }
}
