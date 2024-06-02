using AutoMapper;
using MediatR;
using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.Dtos.Groups;

namespace AMS.Application.UseCases.Group.Command.CreateGroup;

public class CreateGroupHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateGroupCommand, BaseResponse<GroupsDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
        
    public async Task<BaseResponse<GroupsDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GroupsDto>();
        try
        {
            var groupExist = await _unitOfWork.GroupRepository.GroupExistsAsync(request.Description,1);  // cabio  descripcion por name

            if (groupExist > 0)
            {
                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ExceptionMessage.USER_EXISTS;
            }
  
            var group = _mapper.Map<GroupsDto>(request);
            await _unitOfWork.GroupRepository.CreateAsync(group);
                    
            response.Status = (int)ResponseCode.CREATED;
            response.Data = group;
            response.Message = ResponseMessage.GRUPOS_SUCCESS_CREATED;
           
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
}
