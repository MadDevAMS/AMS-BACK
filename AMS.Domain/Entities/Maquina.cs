namespace AMS.Domain.Entities
{
    public class Maquina : BaseEntity
    {
        public Maquina()
        {
            Componentes = new HashSet<Componente>();
        }

        public long IdArea { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
        public ICollection<Componente> Componentes { get; set; }
        public virtual Area Area { get; set; }
    }
}
