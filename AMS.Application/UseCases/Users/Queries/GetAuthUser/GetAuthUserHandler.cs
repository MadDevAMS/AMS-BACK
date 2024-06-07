using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Users.Queries.GetAuthUser
{
    public class GetAuthUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<GetAuthUserQuery, BaseResponse<UserDetailResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<BaseResponse<UserDetailResponseDto>> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserDetailResponseDto>();

            try
            {
                var idUser = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID)!.Value;
                var user = await _unitOfWork.UserRepository.UserByIdAsync(idUser);

                if (user == null)
                {
                    response.Status = (int)ResponseCode.UNAUTHORIZED;
                    response.Message = ExceptionMessage.RESOURCE_NOT_FOUND;
                    return response;
                }

                response.Data = user;
                response.Status = (int)ResponseCode.OK;
                response.Message = ResponseMessage.QUERY_SUCCESS;

            }
            catch (Exception ex)
            {
                response.Status = (int)ResponseCode.CONFLICT;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
