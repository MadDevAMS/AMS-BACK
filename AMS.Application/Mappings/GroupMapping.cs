using AutoMapper;
using AMS.Application.UseCases.Group.Command.CreateGroup;
using AMS.Application.UseCases.Group.Command.UpdateGruop;
using AMS.Domain.Entities;
using AMS.Application.UseCases.Group.Command.DeleteGruop;
using AMS.Application.Dtos.Groups;

namespace AMS.Application.Mappings;

public class GroupMapping : Profile
{
    public GroupMapping()
    {
        CreateMap<DeleteGroupCommand, Group>();
    }
}