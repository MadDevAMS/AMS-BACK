using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Excel;
using MediatR;

namespace AMS.Application;

public class GetVelocityS3Query : IRequest<BaseResponse<VelocityExcelResponseDto>>
{
    public string Key { get; set; } = null!;
}
