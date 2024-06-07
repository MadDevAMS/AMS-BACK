using AMS.Application.UseCases.GroupUsers.Command.CreateGroupUsers;
using AMS.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Mappings
{
    public class GroupUserMapping : Profile
    {
        public GroupUserMapping() 
        {
            CreateMap<CreateGroupUsersCommand, GroupUsers>();
        }
    }
}
