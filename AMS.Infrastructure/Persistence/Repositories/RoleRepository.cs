using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;
    }
}
