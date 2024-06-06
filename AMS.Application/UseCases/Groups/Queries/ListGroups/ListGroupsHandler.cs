using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Groups.Queries.ListGroups
{
    public class ListGroupsHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IRequestHandler<ListGroupsQuery, PaginatorResponse<GroupListDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public async Task<PaginatorResponse<GroupListDto>> Handle(ListGroupsQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatorResponse<GroupListDto>();

            try
            {
                var filter = _mapper.Map<ListGroupFilter>(request);
                string? idEntidadString = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "IdEntidad")?.Value;
                if (long.TryParse(idEntidadString, out long idEntidad))
                {
                    filter.IdEntidad = idEntidad;
                    response = await _unitOfWork.GroupRepository.ListGroups(filter);
                    response.Status = (int)ResponseCode.OK;
                    response.Message = ResponseMessage.QUERY_SUCCESS;
                } else
                {
                    response.Status = (int)ResponseCode.BAD_REQUEST;
                    response.Message = ExceptionMessage.ERROR_PARSE;
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)ResponseCode.BAD_REQUEST;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
