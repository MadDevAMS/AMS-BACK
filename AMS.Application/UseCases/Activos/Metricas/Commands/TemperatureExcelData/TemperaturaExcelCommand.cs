using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using AMS.Application.Dtos.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.TemperatureExcelData
{
    public class TemperaturaExcelCommand : IRequest<BaseResponse<TemperatureExcelResponseDto>>
    {
        public IFormFile File { get; set; } = null!;
        public long MeasurementType { get; set; }

    }
}
