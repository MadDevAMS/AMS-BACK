using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application;

public class GetVelocityS3Handler : IRequestHandler<GetVelocityS3Query, BaseResponse<VelocityExcelResponseDto>>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IS3Files _s3Files;

    public GetVelocityS3Handler(IServiceProvider serviceProvider, IHttpContextAccessor httpContext, IS3Files s3Files)
    {
        _serviceProvider = serviceProvider;
        _httpContext = httpContext;
        _s3Files = s3Files;
    }

    public async Task<BaseResponse<VelocityExcelResponseDto>> Handle(GetVelocityS3Query request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<VelocityExcelResponseDto>();

        using var scope = _serviceProvider.CreateScope();
        var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

        try
        {
            var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

            if (!idEntidad.HasValue)
            {
                response.Status = (int)ResponseCode.UNAUTHORIZED;
                response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                return response;
            }
            var prefix = $"Entidad-{idEntidad}/Metrics/Velocities/{request.Key}";

            var file = await _s3Files.GetFileMemoryStreamAsync(BucketNames.Entidades, prefix);

            var data = director.VelocityExcelMemoryStream(file);

            response.Data = data;
            response.Status = (int)ResponseCode.OK;
            response.Message = ResponseMessage.QUERY_SUCCESS;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
