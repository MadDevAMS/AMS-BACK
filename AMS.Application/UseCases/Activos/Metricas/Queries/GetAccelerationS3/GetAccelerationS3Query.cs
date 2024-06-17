using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;

namespace AMS.Application;

public class GetAccelerationS3Query : IRequest<BaseResponse<AccelerationExcelResponseDto>>
{
    public string Key { get; set; } = null!;
}
