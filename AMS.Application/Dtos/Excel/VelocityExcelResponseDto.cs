namespace AMS.Application.Dtos.Excel
{
    public class VelocityExcelResponseDto : ExcelResponseDto
    {
        public List<float> AxisX { get; set; } = null!;
        public List<float> AxisY { get; set; } = null!;
        public List<float> AxisZ { get; set; } = null!;
    }
}
