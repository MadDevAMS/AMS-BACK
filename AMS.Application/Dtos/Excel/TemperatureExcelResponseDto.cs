namespace AMS.Application.Dtos.Excel
{
    public class TemperatureExcelResponseDto : ExcelResponseDto
    {
        public List<float> Values { get; set; } = new List<float>();
        public List<DateTimeOffset> TimeStamp { get; set; } = new List<DateTimeOffset>();
    }
}
