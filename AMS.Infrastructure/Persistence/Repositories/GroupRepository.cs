using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class GroupRepository(ApplicationDbContext context) : IGroupRepository
    {
        private readonly ApplicationDbContext _context = context;
    }
}
