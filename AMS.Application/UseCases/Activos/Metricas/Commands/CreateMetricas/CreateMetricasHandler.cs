using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas
{
    public class CreateMetricasHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateMetricasCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateMetricasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var metrica = _mapper.Map<Metrica>(request);
                await _unitOfWork.ActivosRepository.CreateMetricasAsync(metrica);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.METRICA_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
