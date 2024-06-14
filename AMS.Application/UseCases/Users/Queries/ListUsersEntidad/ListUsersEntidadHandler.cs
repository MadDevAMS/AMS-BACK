using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.User.Queries.ListUsersEntidad
{
    public class ListUsersEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<ListUsersEntidadQuery, PaginatorResponse<ListUsersResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<PaginatorResponse<ListUsersResponseDto>> Handle(ListUsersEntidadQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatorResponse<ListUsersResponseDto>();

            try
            {
                var filters = _mapper.Map<ListUserFilter>(request);
                var idEntidad = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.ENTIDAD)!.Value;
                var idUser = Functions.GetUserOrEntidadIdFromClaims(_httpContext, Claims.USERID)!.Value;
                filters.IdEntidad = idEntidad;
                response = await _unitOfWork.UserRepository.ListUsersAsync(filters, idUser);
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
