using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina
{
    public class UpdateMaquinaHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateMaquinaCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateMaquinaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var maquina = _mapper.Map<Maquina>(request);
                await _unitOfWork.ActivosRepository.UpdateMaquinaAsync(maquina);
                response.Status = (int)ResponseCode.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
