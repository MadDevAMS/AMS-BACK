namespace AMS.Domain.Entities
{
    public class Metrica : BaseEntity
    {
        public long IdPuntoMonitoreo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }
        public virtual PuntoMonitoreo PuntoMonitoreo { get; set; }
    }
}
