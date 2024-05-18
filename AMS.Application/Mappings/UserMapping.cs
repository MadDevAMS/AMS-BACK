using AMS.Application.UseCases.User.Command.CreateUser;
using AMS.Application.UseCases.User.Command.UpdateUser;
using AMS.Domain.Entities;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommnad, User>();
        }
    }
}
