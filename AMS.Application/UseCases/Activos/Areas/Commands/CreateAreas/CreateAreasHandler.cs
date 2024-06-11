using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas
{
    public class CreateAreasHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<CreateAreasCommand, BaseResponse<AreaDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<AreaDto>> Handle(CreateAreasCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<AreaDto>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var area = _mapper.Map<AreaDto>(request);
                var data = await _unitOfWork.ActivosRepository.CreateAreaAsync(area, userId.Value);

                response.Data = data;
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
