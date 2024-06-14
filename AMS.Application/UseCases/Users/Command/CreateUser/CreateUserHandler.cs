using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;

namespace AMS.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {

                var userExist = await _unitOfWork.UserRepository.UserExistAsync(request.Email);

                if (userExist > 0)
                {
                    response.Status = (int)ResponseCode.CONFLICT;
                    response.Message = ExceptionMessage.USER_EXISTS;
                    return response;
                }

                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.Status = (int)ResponseCode.CONFLICT;
                    response.Message = ExceptionMessage.CONFIRM_PASSWORD;
                    return response;
                }

                var user = _mapper.Map<CreateUserDto>(request);

                var userId = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID);
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD);

                if (!userId.HasValue || !idEntidad.HasValue)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                user.IdEntidad = idEntidad.Value;
                user.Password = BC.HashPassword(user.Password);
                await _unitOfWork.UserRepository.CreateAsync(user, userId.Value);

                response.Status = (int)ResponseCode.CREATED;
                response.Message = ResponseMessage.USER_SUCCESS_REGISTER;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
