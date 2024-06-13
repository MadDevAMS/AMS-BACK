using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.VelocityExcelData
{
    public class VelocityExcelCommand : IRequest<BaseResponse<VelocityExcelResponseDto>>
    {
        public IFormFile File { get; set; } = null!;
    }
}
