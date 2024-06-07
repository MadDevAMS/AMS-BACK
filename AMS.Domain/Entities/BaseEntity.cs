namespace AMS.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public int State { get; set; }
        public long? AuditCreateUser { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public long? AuditUpdateUser { get; set; }
        public DateTime? AuditUpdateDate { get; set; }
        public long? AuditDeleteUser { get; set; }
        public DateTime? AuditDeleteDate { get; set; }
    }
}
