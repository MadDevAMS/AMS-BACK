﻿using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.User;
using AMS.Application.UseCases.User.Command.CreateUser;
using AMS.Application.UseCases.User.Queries.ListUsersEntidad;
using AMS.Application.UseCases.Users.Command.UpdateUser;
using AMS.Domain.Entities;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserCommand, CreateUserDto>();
            CreateMap<ListUsersEntidadQuery, ListUserFilter>();
            CreateMap<UpdateUserCommnad, CreateUserDto>();
        }
    }
}
