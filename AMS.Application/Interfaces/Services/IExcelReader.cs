using AMS.Application.Dtos.Excel;

namespace AMS.Application.Interfaces.Services
{
    public interface IExcelReader
    {
        VelocityResponseDto VelocityExcel(Stream file);
        AccelerationResponseDto AccelerationExcel(Stream file);
        TemperatureResponseDto TemperatureExcel(Stream file);
    }
}
