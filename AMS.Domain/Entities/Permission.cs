namespace AMS.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public Permission()
        {
            GroupPermission = new HashSet<GroupPermission>();
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
    }
}
