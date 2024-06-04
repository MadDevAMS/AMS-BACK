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
        
        public async Task CreateGroupPermissionsAsync(GroupPermissionRegistroDto groupPermissionResponseDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try {
                var groupId = await _context.Groups
                     .Where(g => g.Id == GroupPermissionRegistroDto.GroupId)
                     .FirstOrDefaultAsync();

                if (groupId == null)
                {
                    throw new Exception("Group not found");
                }

                var permissisionId = await _context.Permissions
                    .Where(p => groupPermissionResponseDto.PermissionId.Contains(p.Id))
                    .ToListAsync();

                if (!permissisionId.Any())
                {
                    throw new Exception("No permissions found");
                }

                foreach (var permission in permissisionId)
                {
                    var groupPermission = new GroupPermission   
                    {
                        GroupId = groupId.Id,
                        PermissionId = permission.Id
                    };
                    groupId.GroupPermission.Add(groupPermission);

                    
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception){ 
                await transaction.RollbackAsync();
                throw;
            
            }
        }  
    }
}
