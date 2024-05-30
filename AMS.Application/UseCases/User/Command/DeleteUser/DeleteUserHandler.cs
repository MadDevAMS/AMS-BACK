using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using MediatR;

namespace AMS.Application.UseCases.User.Command.DeleteUser
{
    public class DeleteUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                await _unitOfWork.UserRepository.DeleteAsync(request.Id);
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.DELETE_USER_SUCCESS;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
