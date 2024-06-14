using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.TemperatureExcelData
{
    public class TemperatureExcelHandler : IRequestHandler<TemperaturaExcelCommand, BaseResponse<TemperatureExcelResponseDto>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IS3Files _s3Files;

        public TemperatureExcelHandler(IServiceProvider serviceProvider, IHttpContextAccessor httpContext, IS3Files s3Files)
        {
            _serviceProvider = serviceProvider;
            _httpContext = httpContext;
            _s3Files = s3Files;
        }

        public async Task<BaseResponse<TemperatureExcelResponseDto>> Handle(TemperaturaExcelCommand request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

            var response = new BaseResponse<TemperatureExcelResponseDto>();
            try
            {
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

                if (!idEntidad.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var data = director.TemperatureExcel(request.File!);

                if (request.File is not null)
                {
                    var prefix = $"Entidad-{idEntidad}/Temperatures";

                    bool saveFile = await _s3Files.UploadFileAsync(BucketNames.Entidades, prefix, request.File);

                    if (!saveFile)
                    {
                        response.Status = (int)ResponseCode.BAD_REQUEST;
                        response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                        return response;
                    }
                }


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
