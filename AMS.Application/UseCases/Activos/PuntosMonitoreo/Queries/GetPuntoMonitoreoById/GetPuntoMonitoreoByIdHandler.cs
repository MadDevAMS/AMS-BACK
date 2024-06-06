using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Queries.GetPuntoMonitoreoById
{
    public class GetPuntoMonitoreoByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPuntoMonitoreoByIdQuery, BaseResponse<PuntoMonitoreoResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<PuntoMonitoreoResponseDto>> Handle(GetPuntoMonitoreoByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<PuntoMonitoreoResponseDto>();

            try
            {
                var data = await _unitOfWork.ActivosRepository.GetPuntoMonitoreoByIdAsync(request.Id);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
