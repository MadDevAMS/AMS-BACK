namespace AMS.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IEntidadRepository EntidadRepository { get; }
        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IActivosRepository ActivosRepository { get; }
        Task SaveChanges();
    }
}
