using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo
{
    public class CreatePuntoMonitoreoHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreatePuntoMonitoreoCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreatePuntoMonitoreoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var punto = _mapper.Map<PuntoMonitoreo>(request);
                await _unitOfWork.ActivosRepository.CreatePuntoMonitoreoAsync(punto);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.PUNTO_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
