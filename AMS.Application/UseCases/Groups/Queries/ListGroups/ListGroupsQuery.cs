using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using MediatR;

namespace AMS.Application.UseCases.Groups.Queries.ListGroups
{
    public class ListGroupsQuery: BasePagination, IRequest<PaginatorResponse<GroupListDto>>
    {
    }
}
