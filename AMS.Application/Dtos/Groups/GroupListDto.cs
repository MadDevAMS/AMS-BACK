namespace AMS.Application.Dtos.Groups
{
    public class GroupListDto
    {
        public long GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public List<GroupPermissionListDto> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
    }
}
