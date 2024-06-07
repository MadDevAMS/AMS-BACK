using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserCommnad, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<bool>> Handle(UpdateUserCommnad request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var user = _mapper.Map<Domain.Entities.User>(request);
                await _unitOfWork.UserRepository.UpdateAsync(user, request.UpdateState);

                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.USER_SUCCESS_UPDATE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
