using System.Runtime.CompilerServices;
using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.UseCases.Groups.UpdateGroups;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Groups.UpdateGroup
{
    public class UpdateGroupHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateGroupCommand, BaseResponse<GroupsDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<GroupsDto>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<GroupsDto>();

            try
            {
                var group = _mapper.Map<GroupsDto>(request);
                await _unitOfWork.GroupRepository.UpdateAsync(group);

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
