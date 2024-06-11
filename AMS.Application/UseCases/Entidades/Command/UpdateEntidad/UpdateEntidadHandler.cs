using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<UpdateEntidadCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<bool>> Handle(UpdateEntidadCommand request, CancellationToken cancellationToken)
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

                var entidad = _mapper.Map<Entidad>(request);
                await _unitOfWork.EntidadRepository.UpdateAsync(entidad, userId.Value);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.ENTIDAD_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
