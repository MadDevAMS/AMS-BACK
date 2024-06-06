using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Infrastructure.Services.Excel
{
    public class ExcelReader(IServiceProvider serviceProvider) : IExcelReader
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public AccelerationResponseDto AccelerationExcel(Stream file)
        {
            var accelerationReader = _serviceProvider.GetService<AccelerationExcelReader>();
            return accelerationReader!.ExecuteExcelReader(file);
        }

        public TemperatureResponseDto TemperatureExcel(Stream file)
        {
            var temperatureReader = _serviceProvider.GetService<TemperatureExcelReader>();
            return temperatureReader!.ExecuteExcelReader(file);
        }

        public VelocityResponseDto VelocityExcel(Stream file)
        {
            var velocityReader = _serviceProvider.GetService<VelocityExcelReader>();
            return velocityReader!.ExecuteExcelReader(file);
        }
    }
}
