namespace AMS.Application.Dtos.Groups
{
    public class GroupPermissionListDto
    {
        public long PermissionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
