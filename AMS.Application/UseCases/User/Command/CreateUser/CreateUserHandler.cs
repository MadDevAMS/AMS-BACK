using AMS.Application.Commons.Bases;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace AMS.Application.UseCases.User.Command.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {

                var userExist = await _unitOfWork.UserRepository.UserExistAsync(request.Email);

                if (userExist > 0)
                {
                    response.IsSuccess = false;
                    response.Message = ExceptionMessage.USER_EXISTS;
                    return response;
                }
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = ExceptionMessage.CONFIRM_PASSWORD;
                    return response;
                }

                var user = _mapper.Map<Domain.Entities.User>(request);
                user.Password = BC.HashPassword(user.Password);
                await _unitOfWork.UserRepository.CreateAsync(user);

                response.IsSuccess = true;
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
