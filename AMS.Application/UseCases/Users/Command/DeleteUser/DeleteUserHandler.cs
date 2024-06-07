using System.Security.Claims;
using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.User.Command.DeleteUser
{
    public class DeleteUserHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "IdEntidad")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }
                
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
