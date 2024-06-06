using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.DeleteMaquina
{
    public class DeleteMaquinaHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMaquinaCommmand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<bool>> Handle(DeleteMaquinaCommmand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.ActivosRepository.DeleteMaquinaAsync(request.Id);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.MAQUINA_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
