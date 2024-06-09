using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class ConvertExcelDataCommand : IRequest<BaseResponse<DataExcelResponseDto>>
    {
        public IFormFile File { get; set; } = null!;
    }
}
