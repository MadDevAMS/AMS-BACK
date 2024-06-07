using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMS.Domain.Entities
{
    public class GroupPermission : BaseEntity
    {
        public long GroupPermissionId { get; set; }
        public long GroupId { get; set; }
        public long PermissionId { get; set; }
        public virtual Group Group { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;  
    }
}
