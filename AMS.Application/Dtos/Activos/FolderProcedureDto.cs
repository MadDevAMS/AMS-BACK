namespace AMS.Application.Dtos.Activos
{
    public class FolderProcedureDto
    {
        public long EntidadId { get; set; }
        public string EntidadName { get; set; } = null!;
        public long? AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? ParentAreaId { get; set; }
        public long? MaquinaId { get; set; }
        public string? MaquinaName { get; set; }
        public long? ComponenteId { get; set; }
        public string? ComponenteName { get; set; }
        public long? PuntoMonitoreoId { get; set; }
        public string? PuntoMonitoreoName { get; set; }
        public long? MetricaId { get; set; }
        public string? MetricaName { get; set; }
    }
}
