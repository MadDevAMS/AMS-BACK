using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class AcceleratioExcelCommand : IRequest<BaseResponse<AccelerationExcelResponseDto>>
    {
        public IFormFile File { get; set; } = null!;
    }
}
