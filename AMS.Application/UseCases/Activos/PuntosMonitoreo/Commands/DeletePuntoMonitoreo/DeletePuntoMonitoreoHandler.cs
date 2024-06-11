using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.DeletePuntoMonitoreo
{
    public class DeletePuntoMonitoreoHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : IRequestHandler<DeletePuntoMonitoreoCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<bool>> Handle(DeletePuntoMonitoreoCommand request, CancellationToken cancellationToken)
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

                await _unitOfWork.ActivosRepository.DeletePuntoMonitoreoAsync(request.Id, userId.Value);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.PUNTO_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
