using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina
{
    public class CreateMaquinaHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<CreateMaquinaCommand, BaseResponse<MaquinaDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<MaquinaDto>> Handle(CreateMaquinaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<MaquinaDto>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var maquina = _mapper.Map<MaquinaDto>(request);
                var data = await _unitOfWork.ActivosRepository.CreateMaquinaAsync(maquina, userId.Value);

                response.Data = data;
                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.MAQUINA_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
