using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Groups.Queries.GetGroup
{
    public class GetGroupHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetGroupQuery, BaseResponse<GroupByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<GroupByIdDto>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<GroupByIdDto>();

            try
            {
                var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(request.IdGroup);
                if (group == null) 
                {
                    response.Status = (int)ResponseCode.NOT_FOUND;
                    response.Message = ResponseMessage.RESOURCE_NOT_FOUND;
                } else
                {
                    response.Data = await _unitOfWork.GroupRepository.GetGroupByIdAsync(request.IdGroup);
                    response.Status = (int)ResponseCode.OK;
                    response.Message = ResponseMessage.QUERY_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
