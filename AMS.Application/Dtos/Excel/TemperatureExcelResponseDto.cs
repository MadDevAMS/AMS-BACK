namespace AMS.Application.Dtos.Excel
{
    public class TemperatureExcelResponseDto : ExcelResponseDto
    {
        public List<float> Values { get; set; } = null!;
        public List<DateTimeOffset> TimeStamp { get; set; } = new List<DateTimeOffset>();
    }
}
