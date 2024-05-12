using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository(ApplicationDbContext context) : IPermissionRepository
    {
        private readonly ApplicationDbContext _context = context;
    }
}
