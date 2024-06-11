using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;

namespace AMS.Application.UseCases.Users.Command.UpdateUser
{
    public class UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<UpdateUserCommnad, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;


        public async Task<BaseResponse<bool>> Handle(UpdateUserCommnad request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                if (request.UpdatePassword && !request.Password.Equals(request.ConfirmPassword))
                {
                    response.Status = (int)ResponseCode.CONFLICT;
                    response.Message = ExceptionMessage.CONFIRM_PASSWORD;
                    return response;
                }

                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);

                if (!userId.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                var user = _mapper.Map<CreateUserDto>(request);
                user.Password = BC.HashPassword(user.Password);
                await _unitOfWork.UserRepository.UpdateAsync(user, request.UpdateState, request.UpdatePassword, userId.Value);

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
