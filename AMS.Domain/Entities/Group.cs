namespace AMS.Domain.Entities
{
    public sealed class Group : BaseEntity
    {
        public Group()
        {
            GroupUsers = new HashSet<GroupUsers>();
        }

        public string Description { get; set; } = null!;
        public ICollection<GroupUsers> GroupUsers { get; set; }

    }
}