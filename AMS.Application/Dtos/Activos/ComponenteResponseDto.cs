namespace AMS.Application.Dtos.Activos
{
    public class ComponenteResponseDto
    {
        public long Id { get; set; }  
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
    }
}
