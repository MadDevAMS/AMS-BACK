using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class ConvertAccelerationHandler : IRequestHandler<ConvertAccelerationCommand, BaseResponse<DataExcelResponseDto>>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConvertAccelerationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<BaseResponse<DataExcelResponseDto>> Handle(ConvertAccelerationCommand request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

            var response = new BaseResponse<DataExcelResponseDto>();

            try
            {
                var data = director.MeasurementExcel(request.File);
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
