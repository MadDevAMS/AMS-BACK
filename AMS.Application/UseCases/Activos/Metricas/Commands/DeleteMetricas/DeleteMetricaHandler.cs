﻿using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.DeleteMetricas
{
    public class DeleteMetricaHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : IRequestHandler<DeleteMetricaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<bool>> Handle(DeleteMetricaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                await _unitOfWork.ActivosRepository.DeleteMetricaAsync(request.Id, userId.Value);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.METRICA_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
