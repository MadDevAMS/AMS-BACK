using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.DeleteMetricas
{
    public class DeleteMetricaHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMetricaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<bool>> Handle(DeleteMetricaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.ActivosRepository.DeleteMetricaAsync(request.Id);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.METRICA_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
