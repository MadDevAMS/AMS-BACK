namespace AMS.Application.Dtos.Activos
{
    public class PuntoMonitoreoResponseDto
    {
        public long IdPuntoMonitoreo { get; set; }
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string DireccionMedicion { get; set; } = null!;
        public string AnguloDireccion { get; set; } = null!;
        public string DatosMedicion { get; set; } = null!;
    }
}
