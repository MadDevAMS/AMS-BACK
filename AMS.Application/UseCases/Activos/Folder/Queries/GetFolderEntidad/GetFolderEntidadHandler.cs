using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Folder.Queries.GetFolderEntidad
{
    public class GetFolderEntidadHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : IRequestHandler<GetFolderEntidadQuery, BaseResponse<FolderResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<FolderResponseDto>> Handle(GetFolderEntidadQuery request, CancellationToken cancellationToken)
        {
            var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD)!.Value;
            var response = await _unitOfWork.ActivosRepository.GetFolderById(idEntidad);

            try
            {
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
