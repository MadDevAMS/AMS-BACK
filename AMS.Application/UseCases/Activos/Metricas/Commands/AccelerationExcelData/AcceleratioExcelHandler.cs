using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class AcceleratioExcelHandler : IRequestHandler<AcceleratioExcelCommand, BaseResponse<AccelerationExcelResponseDto>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IS3Files _s3Files;

        public AcceleratioExcelHandler(IServiceProvider serviceProvider, IHttpContextAccessor httpContext, IS3Files s3Files)
        {
            _serviceProvider = serviceProvider;
            _httpContext = httpContext;
            _s3Files = s3Files;
        }

        public async Task<BaseResponse<AccelerationExcelResponseDto>> Handle(AcceleratioExcelCommand request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

            var response = new BaseResponse<AccelerationExcelResponseDto>();
            try
            {
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

                if (!idEntidad.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                if (request.File is not null)
                {
                    var prefix = $"Entidad-{idEntidad}/Accelerations";

                    bool saveFile = await _s3Files.UploadFileAsync(BucketNames.ExcelMetricas, prefix, request.File);

                    if (!saveFile)
                    {
                        response.Status = (int)ResponseCode.BAD_REQUEST;
                        response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                        return response;
                    }
                }
                var data = director.AccelerationExcel(request.File!);

                response.Data = data;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }

            return response;
        }
    }
}
