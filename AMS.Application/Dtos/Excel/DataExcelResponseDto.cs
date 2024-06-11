namespace AMS.Application.Dtos.Excel
{
    public class DataExcelResponseDto
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
        public List<AxisResponseDto> AxisX { get; set; } = new List<AxisResponseDto>();
        public List<AxisResponseDto> AxisY { get; set; } = new List<AxisResponseDto>();
        public List<AxisResponseDto> AxisZ { get; set; } = new List<AxisResponseDto>();
    }
}

