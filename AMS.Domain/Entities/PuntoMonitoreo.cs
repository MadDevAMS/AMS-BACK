namespace AMS.Domain.Entities
{
    public class PuntoMonitoreo : BaseEntity
    {
        public PuntoMonitoreo()
        {
            Metricas = new HashSet<Metrica>();
        }

        public long IdComponente { get; set; }
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string DireccionMedicion { get; set; } = null!;
        public string AnguloDireccion { get; set; } = null!;
        public string DatosMedicion { get; set; } = null!;
        public ICollection<Metrica> Metricas { get; set; }
        public virtual Componente Componente { get; set; }
    }
}
