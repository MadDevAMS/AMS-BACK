using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.DeletePuntoMonitoreo
{
    public class DeletePuntoMonitoreoHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePuntoMonitoreoCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<bool>> Handle(DeletePuntoMonitoreoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.ActivosRepository.DeletePuntoMonitoreoAsync(request.Id);
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
