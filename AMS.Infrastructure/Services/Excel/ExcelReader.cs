using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Infrastructure.Services.Excel
{
    public class ExcelReader(IServiceProvider serviceProvider) : IExcelReader
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public DataExcelResponseDto MeasurementExcel(IFormFile file)
        {
            var accelerationReader = _serviceProvider.GetService<MeasurementExcelReader>();
            return accelerationReader!.ExecuteExcelReader(file);
        }
    }
}
