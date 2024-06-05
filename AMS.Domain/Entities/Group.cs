namespace AMS.Domain.Entities
{
    public sealed class Group : BaseEntity
    {
        public Group()
        {
            GroupPermission = new HashSet<GroupPermission>();
            GroupUsers = new HashSet<GroupUsers>();
        }

        public long GroupId { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public long IdEntidad { get; set; }
        public ICollection<GroupPermission> GroupPermission { get; set; }
        public ICollection<GroupUsers> GroupUsers { get; set; }

    }
}