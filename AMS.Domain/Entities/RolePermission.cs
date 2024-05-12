namespace AMS.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
