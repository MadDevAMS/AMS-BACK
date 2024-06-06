using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.DeleteComponente
{
    public class DeleteComponenteHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteComponenteCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(DeleteComponenteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.ActivosRepository.DeleteComponenteAsync(request.Id);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.COMPONENTE_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
