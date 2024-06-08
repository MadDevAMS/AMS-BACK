using AMS.Application.Dtos.Excel;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.Interfaces.Services
{
    public interface IExcelReader
    {
        DataExcelResponseDto MeasurementExcel(IFormFile file);
    }
}
