using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Interfaces;
using AMS.Application.Commons.Utils;
using AMS.Application.Interfaces.Persistence;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace AMS.Application.UseCases.User.Command.Login
{
    public class LoginHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<BaseResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>();

            try
            {
                var user = await _unitOfWork.UserRepository.UserByEmailAsync(request.Email);
                if (user == null)
                {
                    response.Status = (int)ResponseCode.NOT_FOUND;
                    response.Message = ExceptionMessage.INVALID_CREDENTIALS;
                    return response;
                }

                if (BC.Verify(request.Password, hash: user.Password))
                {
                    response.Status = (int)ResponseCode.OK;
                    response.Data = _jwtTokenGenerator.GenerateToken(user);
                    response.Message = ResponseMessage.LOGIN_SUCCESS;
                    return response;
                }

                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ExceptionMessage.INVALID_CREDENTIALS;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
