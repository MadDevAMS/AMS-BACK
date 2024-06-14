﻿using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Excel;
using AMS.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration
{
    public class AcceleratioExcelHandler : IRequestHandler<AcceleratioExcelCommand, BaseResponse<AccelerationExcelResponseDto>>
    {
        private readonly IServiceProvider _serviceProvider;

        public AcceleratioExcelHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<BaseResponse<AccelerationExcelResponseDto>> Handle(AcceleratioExcelCommand request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var director = scope.ServiceProvider.GetRequiredService<IExcelReader>();

            var response = new BaseResponse<AccelerationExcelResponseDto>();
            try
            {
                var data = director.AccelerationExcel(request.File);

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