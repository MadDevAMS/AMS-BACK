using AutoMapper;

using AMS.Domain.Entities;
using AMS.Application.UseCases.Group.Command.DeleteGruop;

namespace AMS.Application.Mappings;

public class GroupMapping : Profile
{
    public GroupMapping()
    {
        CreateMap<DeleteGroupCommand, Group>();
    }
}