using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Persistence.Repositories;

namespace AMS.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IUserRepository _user = null!;
        private readonly IEntidadRepository _entidad = null!;
        private readonly IGroupRepository _group = null!;
        private readonly IActivosRepository _activos = null!;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _user ?? new UserRepository(_context);
        public IEntidadRepository EntidadRepository => _entidad ?? new EntidadRepository(_context);
        public IGroupRepository GroupRepository => _group ?? new GroupRepository(_context);
        public IActivosRepository ActivosRepository => _activos ?? new ActivosRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
