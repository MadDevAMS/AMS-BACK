using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Queries.GetMaquina
{
    public class GetMaquinaHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMaquinaQuery, BaseResponse<MaquinaResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<MaquinaResponseDto>> Handle(GetMaquinaQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<MaquinaResponseDto>();

            try
            {
                var data = await _unitOfWork.ActivosRepository.GetMaquinaByIdAsync(request.Id);
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
