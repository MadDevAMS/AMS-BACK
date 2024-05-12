using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
    }
}
