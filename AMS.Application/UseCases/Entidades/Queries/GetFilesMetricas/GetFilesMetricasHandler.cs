using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application;

public class GetFilesMetricasHandler(IS3Files s3Files, IHttpContextAccessor httpContext) : IRequestHandler<GetFilesMetricasQuery, BaseResponse<IEnumerable<S3ObjectDto>>>
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IS3Files _s3Files = s3Files;

    public async Task<BaseResponse<IEnumerable<S3ObjectDto>>> Handle(GetFilesMetricasQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<S3ObjectDto>>();

        try
        {
            var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

            if (!idEntidad.HasValue)
            {
                response.Status = (int)ResponseCode.UNAUTHORIZED;
                response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                return response;
            }

            var prefix = $"Entidad-{idEntidad}/Metrics";
            var data = await _s3Files.GetFilesEntidadAsync(BucketNames.Entidades, prefix);

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
