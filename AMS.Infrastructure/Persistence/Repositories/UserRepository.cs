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
    }
}
