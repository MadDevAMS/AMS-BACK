using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina
{
    public class CreateMaquinaHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateMaquinaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateMaquinaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var maquina = _mapper.Map<Maquina>(request);
                await _unitOfWork.ActivosRepository.CreateMaquinaAsync(maquina);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseActivosMessage.MAQUINA_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
