namespace AMS.Domain.Entities
{
    public class Entidad : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }
}
