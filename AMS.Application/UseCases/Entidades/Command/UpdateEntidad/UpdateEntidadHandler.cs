using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateEntidadCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateEntidadCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var entidad = _mapper.Map<Entidad>(request);
                await _unitOfWork.EntidadRepository.UpdateAsync(entidad);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.ENTIDAD_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
