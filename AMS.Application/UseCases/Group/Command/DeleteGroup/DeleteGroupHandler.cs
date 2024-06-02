using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Group.Command.DeleteGruop
{
    public class DeleteGroupHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<DeleteGroupCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {

                var group = _mapper.Map<Domain.Entities.Group>(request);

                await _unitOfWork.GroupRepository.DeleteAsync(group.Id);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.USER_SUCCESS_DELETE;
                    return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
