using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo
{
    public class UpdatePuntoMonitoreoHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdatePuntoMonitoreoCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdatePuntoMonitoreoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var punto = _mapper.Map<PuntoMonitoreo>(request);
                await _unitOfWork.ActivosRepository.UpdatePuntoMonitorioAsync(punto);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.PUNTO_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }

}
