using AMS.Application.Dtos.GroupPermission;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class GroupPermissionRepository(ApplicationDbContext context) : IGroupPermissionRepository 
    {
        private readonly ApplicationDbContext _context = context;
        
        public async Task CreateGroupPermissionsAsync(GroupPermission groupPermission)
        {
            groupPermission.State = Utils.ESTADO_ACTIVO;
            groupPermission.AuditCreateDate = DateTime.Now;
            
            await _context.AddAsync(groupPermission);
            await _context.SaveChangesAsync();
        }
    }
}
