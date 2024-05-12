namespace AMS.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            RoleUsers = new HashSet<RoleUser>();
        }

        public string Description { get; set; } = null!;
        public long IdEntidad { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
