namespace AMS.Application.Dtos.Activos
{
    public class AreaDto
    {
        public long IdArea { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long IdParent { get; set; }
        public long IdEntidad { get; set; }
    }
}
