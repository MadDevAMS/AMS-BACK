using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Queries.MetricaById
{
    public class MetricaByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<MetricaByIdQuery, BaseResponse<MetricasResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<MetricasResponseDto>> Handle(MetricaByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<MetricasResponseDto>();

            try
            {
                var metrica = await _unitOfWork.ActivosRepository.GetMetricaByIdAsync(request.Id);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
                response.Data = metrica;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
