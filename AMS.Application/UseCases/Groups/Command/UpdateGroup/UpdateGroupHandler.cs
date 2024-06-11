using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Groups.Command.UpdateGroup
{
    public class UpdateGroupHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<UpdateGroupCommand, BaseResponse<GroupsDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<GroupsDto>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<GroupsDto>();

            try
            {
                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var group = _mapper.Map<GroupsDto>(request);
                await _unitOfWork.GroupRepository.UpdateAsync(group, userId.Value);

                response.Status = (int)ResponseCode.OK;
                response.Data = group;
                response.Message = ResponseMessage.GROUP_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
