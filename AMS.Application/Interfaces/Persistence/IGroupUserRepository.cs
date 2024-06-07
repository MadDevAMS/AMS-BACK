using AMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IGroupUserRepository
    {
        Task CreateGroupUsers(GroupUsers groupUsers);
    }
}
