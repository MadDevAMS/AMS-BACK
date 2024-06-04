using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas
{
    internal class UpdateAreasHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAreasCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateAreasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var area = _mapper.Map<Area>(request);
                await _unitOfWork.ActivosRepository.UpdateAreaAsync(area);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.AREA_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

            }

            return response;
        }
    }
}
