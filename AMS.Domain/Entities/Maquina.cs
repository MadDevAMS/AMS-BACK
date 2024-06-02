namespace AMS.Domain.Entities
{
    public class Maquina : BaseEntity
    {
        public Maquina()
        {
            Componentes = new HashSet<Componente>();
        }

        public long IdArea { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TipoMaquina { get; set; }
        public ICollection<Componente> Componentes { get; set; }
        public virtual Area Area { get; set; }
    }
}
