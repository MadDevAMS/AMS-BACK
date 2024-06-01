using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Permissions;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.User.Queries.ListUsersEntidad
{
    public class ListUsersEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ListUsersEntidadQuery, PaginatorResponse<ListUsersResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatorResponse<ListUsersResponseDto>> Handle(ListUsersEntidadQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatorResponse<ListUsersResponseDto>();

            try
            {
                var filters = _mapper.Map<ListUserFilter>(request);
                response = await _unitOfWork.UserRepository.ListUsersAsync(filters);
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
