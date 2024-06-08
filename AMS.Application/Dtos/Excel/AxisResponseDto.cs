namespace AMS.Application.Dtos.Excel
{
    public class AxisResponseDto
    {
        public DateTimeOffset TimeStamp { get; set; }
        public string Value { get; set; } = null!;
        public string Axis { get; set; } = null!;
        public string AxisLabel { get; set; } = null!;
    }
}
