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
