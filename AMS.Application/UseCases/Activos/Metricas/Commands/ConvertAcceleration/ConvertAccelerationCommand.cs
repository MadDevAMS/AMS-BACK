using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class ConvertAccelerationCommand : IRequest<BaseResponse<DataExcelResponseDto>>
    {
        public IFormFile File { get; set; } = null!;
    }
}
