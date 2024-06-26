﻿using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using AMS.Infrastructure.Services.Excel.FormFile;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Infrastructure.Services.Excel
{
    public class ExcelReader(IServiceProvider serviceProvider) : IExcelReader
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public AccelerationExcelResponseDto AccelerationExcel(IFormFile file)
        {
            var accelerationReader = _serviceProvider.GetService<AccelerationExcelReader>();
            return accelerationReader!.ExecuteExcelReader(file);
        }
        public VelocityExcelResponseDto VelocityExcel(IFormFile file)
        {
            var velocityReader = _serviceProvider.GetService<VelocitExcelReader>();
            return velocityReader!.ExecuteExcelReader(file);
        }

        public TemperatureExcelResponseDto TemperatureExcel(IFormFile file)
        {
            var temperatureReader = _serviceProvider.GetService<TemperatureExcelReader>();
            return temperatureReader!.ExecuteExcelReader(file);
        }
        public AccelerationExcelResponseDto AccelerationExcelMemoryStream(MemoryStream file)
        {
            var accelerationReader = _serviceProvider.GetService<AccelarationExcelMemory>();
            return accelerationReader!.ExecuteExcelReaderMemory(file);
        }

        public TemperatureExcelResponseDto TemperatureExcelMemoryStream(MemoryStream file)
        {
            var temperatureReader = _serviceProvider.GetService<TemperatureExcelMemory>();
            return temperatureReader!.ExecuteExcelReaderMemory(file);
        }

        public VelocityExcelResponseDto VelocityExcelMemoryStream(MemoryStream file)
        {
            var velocityReader = _serviceProvider.GetService<VelocityExcelMemory>();
            return velocityReader!.ExecuteExcelReaderMemory(file);
        }
    }
}
