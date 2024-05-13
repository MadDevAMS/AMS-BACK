using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class EntidadRepository(ApplicationDbContext context) : IEntidadRepository
    {
        private readonly ApplicationDbContext _context = context;
    }
}
