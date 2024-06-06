namespace AMS.Application.Dtos.Groups
{
    public class GroupUpdateDto
    {
        public long GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<long> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
        public int State { get; set; }
    }
}
