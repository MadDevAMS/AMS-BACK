namespace AMS.Application.Dtos.Excel
{
    public class VelocityExcelResponseDto : ExcelResponseDto
    {
        public float Rms { get; set; }
        public List<float> AxisX { get; set; } = new List<float>();
        public List<float> AxisY { get; set; } = new List<float>();
        public List<float> AxisZ { get; set; } = new List<float>();
        public List<DateTimeOffset> TimeStamp { get; set; } = new List<DateTimeOffset>();
    }
}
