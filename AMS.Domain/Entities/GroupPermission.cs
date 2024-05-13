namespace AMS.Domain.Entities
{
    public class GroupPermission : BaseEntity
    {
        public long GroupId { get; set; }
        public long PermissionId { get; set; }
        public virtual Group Group{ get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
