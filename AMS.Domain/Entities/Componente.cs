namespace AMS.Domain.Entities
{
    public class Componente : BaseEntity
    {
        public Componente()
        {
            PuntosMonitoreo = new HashSet<PuntoMonitoreo>();
        }

        public long IdMaquina { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
        public ICollection<PuntoMonitoreo> PuntosMonitoreo { get; set; }
        public virtual Maquina Maquina { get; set; }
    }
}
