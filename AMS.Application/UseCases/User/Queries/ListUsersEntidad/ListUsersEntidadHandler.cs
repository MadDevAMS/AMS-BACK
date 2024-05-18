using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace AMS.Application.UseCases.User.Queries.ListUsersEntidad
{
    public class ListUsersEntidadHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ListUsersEntidadQuery, BaseResponse<IEnumerable<ListUsersResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<ListUsersResponseDto>>> Handle(ListUsersEntidadQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<ListUsersResponseDto>>();

            try
            {
                var filter = _mapper.Map<ListUserFilter>(request);
                var users = await _unitOfWork.UserRepository.ListUsersAsync(filter);

                response.IsSuccess = true;
                response.Message = ResponseMessage.QUERY_SUCCESS;
                response.TotalRecords = users.Count;
                response.Data = users;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


    }
}
