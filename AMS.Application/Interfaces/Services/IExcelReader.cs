using AMS.Application.Dtos.Excel;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.Interfaces.Services
{
    public interface IExcelReader
    {
        AccelerationExcelResponseDto AccelerationExcel(IFormFile file);
        TemperatureExcelResponseDto TemperatureExcel(IFormFile file);
        VelocityExcelResponseDto VelocityExcel(IFormFile file);
        AccelerationExcelResponseDto AccelerationExcelMemoryStream(MemoryStream file);
        TemperatureExcelResponseDto TemperatureExcelMemoryStream(MemoryStream file);
        VelocityExcelResponseDto VelocityExcelMemoryStream(MemoryStream file);
    }
}
