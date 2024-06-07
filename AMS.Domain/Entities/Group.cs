namespace AMS.Domain.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            GroupPermission = new HashSet<GroupPermission>();
            GroupUsers = new HashSet<GroupUsers>();
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public long IdEntidad { get; set; }
        public virtual Entidad Entidad { get; set; }
        public ICollection<GroupPermission> GroupPermission { get; set; }
        public ICollection<GroupUsers> GroupUsers { get; set; }

    }
}