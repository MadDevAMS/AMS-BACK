using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using MediatR;

namespace AMS.Application.UseCases.Groups.Queries.GetGroup
{
    public class GetGroupQuery: IRequest<BaseResponse<GroupByIdDto>>
    {
        public long IdGroup { get; set; }
    }
}
