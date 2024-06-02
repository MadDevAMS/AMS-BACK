using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AutoMapper;
using MediatR;


namespace AMS.Application.UseCases.Group.Command.UpdateGruop
{
    public class UpdateGroupHandler(IUnitOfWork unitOfWork,IMapper mapper)
        : IRequestHandler<UpdateGroupCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var grupos = _mapper.Map<Domain.Entities.Group>(request);
                await _unitOfWork.GroupRepository.UpdateAsync(grupos);
                
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.GRUPOS_SUCCESS_UPDATE;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;

        }

    }
}
