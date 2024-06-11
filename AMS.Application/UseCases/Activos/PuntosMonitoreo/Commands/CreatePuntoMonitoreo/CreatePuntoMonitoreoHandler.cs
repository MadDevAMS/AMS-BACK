using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo
{
    public class CreatePuntoMonitoreoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<CreatePuntoMonitoreoCommand, BaseResponse<PuntoMonitoreoDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<PuntoMonitoreoDto>> Handle(CreatePuntoMonitoreoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<PuntoMonitoreoDto>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var punto = _mapper.Map<PuntoMonitoreoDto>(request);
                var data = await _unitOfWork.ActivosRepository.CreatePuntoMonitoreoAsync(punto, userId.Value);

                response.Data = data;
                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.PUNTO_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
