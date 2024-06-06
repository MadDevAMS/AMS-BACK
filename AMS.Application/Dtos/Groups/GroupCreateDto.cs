namespace AMS.Application.Dtos.Groups
{
    public class GroupCreateDto
    {
        public long IdEntidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<long> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
    }
}
