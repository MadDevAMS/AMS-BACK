using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.TemperatureExcelData
{
    public class TemperatureExcelHandler : IRequestHandler<TemperaturaExcelCommand, BaseResponse<TemperatureExcelResponseDto>>
    {
        private readonly IServiceProvider _serviceProvider;

        public TemperatureExcelHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<BaseResponse<TemperatureExcelResponseDto>> Handle(TemperaturaExcelCommand request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

            var response = new BaseResponse<TemperatureExcelResponseDto>();
            try
            {
                var data = director.TemperatureExcel(request.File);

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
