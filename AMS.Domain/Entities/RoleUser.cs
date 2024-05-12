namespace AMS.Domain.Entities
{
    public class RoleUser : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
