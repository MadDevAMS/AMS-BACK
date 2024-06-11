using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Entidad;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Entidades.Queries.GetEntidad
{
    public class GetEntidadHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : IRequestHandler<GetEntidadQuery, BaseResponse<EntidadDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<EntidadDto>> Handle(GetEntidadQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<EntidadDto>();

            try
            {
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD)!.Value;
                var entidad = await _unitOfWork.EntidadRepository.GetEntidadAsync(idEntidad);

                if (entidad == null)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                response.Data = entidad;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
