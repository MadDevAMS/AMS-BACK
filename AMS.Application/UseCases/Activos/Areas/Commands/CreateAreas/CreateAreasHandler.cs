using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas
{
    public class CreateAreasHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAreasCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(CreateAreasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var area = _mapper.Map<Area>(request);
                await _unitOfWork.ActivosRepository.CreateAreaAsync(area);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseActivosMessage.AREA_SUCCESS_REGISTER;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
