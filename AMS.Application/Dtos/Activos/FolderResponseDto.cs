namespace AMS.Application.Dtos.Activos
{
    public class FolderResponseDto
    {
        public long EntidadId { get; set; }
        public string EntidadName { get; set; } = null!;
        public List<AreaFolderDto> Areas { get; set; }
    }

    public class AreaFolderDto
    {
        public long AreaId { get; set; }
        public string AreaName { get; set; }
        public List<AreaFolderDto> SubAreas { get; set; }
        public List<MaquinaFolderDto> Maquinas { get; set; }
    }

    public class MaquinaFolderDto
    {
        public long MaquinaId { get; set; }
        public string MaquinaName { get; set; }
        public List<ComponenteFolderDto> Componentes { get; set; }
    }

    public class ComponenteFolderDto
    {
        public long ComponenteId { get; set; }
        public string ComponenteName { get; set; }
        public List<PuntoMonitoreoFolderDto> PuntosMoniteros { get; set; }

    }

    public class PuntoMonitoreoFolderDto
    {
        public long PuntoMonitoreoId { get; set; }
        public string PuntoMonitoreoName { get; set; }
        public List<MetricaFolderDto> Metricas { get; set; }
    }


    public class MetricaFolderDto
    {
        public long MetricaId { get; set; }
        public string MetricaName { get; set; }
    }
}
