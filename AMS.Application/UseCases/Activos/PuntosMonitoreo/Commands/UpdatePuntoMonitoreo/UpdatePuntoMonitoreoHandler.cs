using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo
{
    public class UpdatePuntoMonitoreoHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<UpdatePuntoMonitoreoCommand, BaseResponse<PuntoMonitoreoDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<PuntoMonitoreoDto>> Handle(UpdatePuntoMonitoreoCommand request, CancellationToken cancellationToken)
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
                var data = await _unitOfWork.ActivosRepository.UpdatePuntoMonitorioAsync(punto, userId.Value);

                response.Data = data;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.PUNTO_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }

}
