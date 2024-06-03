using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente
{
    public class CreateComponenteHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateComponenteCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateComponenteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var componente = _mapper.Map<Componente>(request);
                await _unitOfWork.ActivosRepository.CreateComponenteAsync(componente);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.COMPONENTE_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
