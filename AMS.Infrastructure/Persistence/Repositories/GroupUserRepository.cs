using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class GroupUserRepository(ApplicationDbContext context) : IGroupUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task CreateGroupUsers(GroupUsers groupUsers)
        {
            groupUsers.State = Utils.ESTADO_ACTIVO;
            groupUsers.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(groupUsers);
            await _context.SaveChangesAsync();
        }
    }
}
