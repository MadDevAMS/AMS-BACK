using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Queries.GetComponente
{
    public class GetComponenteHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetComponenteQuery, BaseResponse<ComponenteResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<ComponenteResponseDto>> Handle(GetComponenteQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ComponenteResponseDto>();

            try
            {
                var data = await _unitOfWork.ActivosRepository.GetComponenteByIdAsync(request.Id);
                response.Data = data;
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
