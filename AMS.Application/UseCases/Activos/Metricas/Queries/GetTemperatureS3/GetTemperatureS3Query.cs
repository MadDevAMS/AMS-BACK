using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;

namespace AMS.Application;

public class GetTemperatureS3Query : IRequest<BaseResponse<TemperatureExcelResponseDto>>
{
    public string Key { get; set; } = null!;
}
