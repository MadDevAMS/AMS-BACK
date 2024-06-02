namespace AMS.Domain.Entities
{
    public class Area : BaseEntity
    {
        public Area()
        {
            Maquinas = new HashSet<Maquina>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public long IdParent { get; set; }
        public long IdEntidad { get; set; }
        public ICollection<Maquina> Maquinas { get; set; }
        public virtual Entidad Entidad { get; set; }
    }
}
