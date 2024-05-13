namespace AMS.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IEntidadRepository EntidadRepository { get; }
        IUserRepository UserRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IGroupRepository GroupRepository { get; }
        Task SaveChanges();
    }
}
