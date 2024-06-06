using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente
{
    public class UpdateComponenteHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateComponenteCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateComponenteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var componente = _mapper.Map<Componente>(request);
                await _unitOfWork.ActivosRepository.UpdateComponenteAsync(componente);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.COMPONENTE_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
