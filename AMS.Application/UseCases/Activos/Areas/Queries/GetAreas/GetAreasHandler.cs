using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Queries.GetAreas
{
    public class GetAreasHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAreasQuery, BaseResponse<AreaDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<AreaDto>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<AreaDto>();

            try
            {
                var data = await _unitOfWork.ActivosRepository.GetAreaByIdAsync(request.Id);

                response.Data = data;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
