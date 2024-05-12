namespace AMS.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public string Description { get; set; } = null!;
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
