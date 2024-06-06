using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Folder.Queries.GetFolderEntidad
{
    public class GetFolderEntidadHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetFolderEntidadQuery, BaseResponse<FolderResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<FolderResponseDto>> Handle(GetFolderEntidadQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.ActivosRepository.GetFolderById(2);

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
