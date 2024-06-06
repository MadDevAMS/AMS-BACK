namespace AMS.Application.Dtos.Activos
{
    public class MaquinaResponseDto
    {
        public long IdMaquina { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
    }
}
