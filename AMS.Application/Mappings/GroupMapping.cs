using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.UseCases.Groups.Command.CreateGroup;
using AMS.Application.UseCases.Groups.Command.UpdateGroup;
using AMS.Application.UseCases.Groups.Queries.ListGroups;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class GroupMapping: Profile
    {
        public GroupMapping() 
        {
            CreateMap<UpdateGroupCommand, GroupsDto>();
            CreateMap<CreateGroupCommand, GroupsDto>();
            CreateMap<ListGroupsQuery, ListGroupFilter>();
        }
    }
}
