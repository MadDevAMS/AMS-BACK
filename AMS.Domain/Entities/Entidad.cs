namespace AMS.Domain.Entities
{
    public class Entidad : BaseEntity
    {
        public Entidad()
        {
            Users = new HashSet<User>();
            Groups = new HashSet<Group>();
            Areas = new HashSet<Area>();
        }

        public string Nombre { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public ICollection<User> Users { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
