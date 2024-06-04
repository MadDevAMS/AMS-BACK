using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.DeleteAreas
{
    public class DeleteAreasHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteAreasCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<bool>> Handle(DeleteAreasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.ActivosRepository.DeleteAreaAsync(request.Id);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.AREA_SUCCESS_DELETE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
