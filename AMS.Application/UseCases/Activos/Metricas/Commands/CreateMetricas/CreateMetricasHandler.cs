using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas
{
    public class CreateMetricasHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<CreateMetricasCommand, BaseResponse<MetricasDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<MetricasDto>> Handle(CreateMetricasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<MetricasDto>();
            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var metrica = _mapper.Map<MetricasDto>(request);
                var data = await _unitOfWork.ActivosRepository.CreateMetricasAsync(metrica, userId.Value);

                response.Data = data;
                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.METRICA_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
