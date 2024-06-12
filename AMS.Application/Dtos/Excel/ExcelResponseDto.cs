namespace AMS.Application.Dtos.Excel
{
    public abstract class ExcelResponseDto
    {
        public string MeasurementType { get; set; } = null!;
        public string SpotId { get; set; } = null!;
        public string SpotDyid { get; set; } = null!;
        public string SpotName { get; set; } = null!;
        public string SpotType { get; set; } = null!;
        public string SpotRpm { get; set; } = null!;
        public string SpotModel { get; set; } = null!;
        public string MachineId { get; set; } = null!;
        public string MachineName { get; set; } = null!;
    }
}
