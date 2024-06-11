namespace AMS.Application.Dtos.Activos
{
    public class MetricasDto
    {
        public long IdMetrica { get; set; }
        public long IdPuntoMonitoreo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }
    }
}
