namespace AMS.Application.Dtos.Activos
{
    public class FolderResponseDto
    {
        public long EntidadId { get; set; }
        public string EntidadName { get; set; } = null!;
        public List<AreaDto> Areas { get; set; }
    }

    public class AreaDto
    {
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public List<AreaDto> SubAreas { get; set; }
        public List<MaquinaDto> Maquinas { get; set; }
    }

    public class MaquinaDto
    {
        public long MaquinaId { get; set; }
        public string MaquinaName { get; set; }
        public List<ComponenteDto> Componentes { get; set; }
    }

    public class ComponenteDto
    {
        public long ComponenteId { get; set; }
        public string ComponenteName { get; set; }
        public List<PuntoMonitoreoDto> PuntosMoniteros{ get; set; }

    }

    public class PuntoMonitoreoDto
    {
        public long PuntoMonitoreoId { get; set; }
        public string PuntoMonitoreoName { get; set; }
        public List<MetricaDto> Metricas { get; set; }
    }


    public class MetricaDto
    {
        public long MetricaId { get; set; }
        public string MetricaName { get; set; }
    }
}
