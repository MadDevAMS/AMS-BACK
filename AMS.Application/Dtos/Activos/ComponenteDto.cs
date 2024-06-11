namespace AMS.Application.Dtos.Activos
{
    public class ComponenteDto
    {
        public long IdComponente { get; set; }
        public long IdMaquina { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
    }
}
