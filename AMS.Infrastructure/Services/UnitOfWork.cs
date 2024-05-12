using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Persistence.Repositories;

namespace AMS.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IUserRepository _user = null!;
        private readonly IRoleRepository _role = null!;
        private readonly IPermissionRepository _permission = null!;
        private readonly IGroupRepository _group = null!;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _user ?? new UserRepository(_context);
        public IRoleRepository RoleRepository => _role ?? new RoleRepository(_context);
        public IPermissionRepository PermissionRepository => _permission ?? new PermissionRepository(_context);
        public IGroupRepository GroupRepository => _group ?? new GroupRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
