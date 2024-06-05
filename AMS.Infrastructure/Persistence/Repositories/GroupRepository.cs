using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class GroupRepository(ApplicationDbContext context) : IGroupRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task UpdateAsync(GroupsDto group)
        {
            var entityUpdate = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.GroupId) ?? throw new Exception("Group not found");
            var currentGroupUsers = await _context.GroupUsers.Where(gu => gu.GroupId == group.GroupId).ToListAsync();
            var currentGroupPermissions = await _context.GroupPermission.Where(gu => gu.GroupId == group.GroupId).ToListAsync();

            entityUpdate.Name = group.Name;
            entityUpdate.Description = group.Description;
            entityUpdate.State = group.State;
            entityUpdate.AuditUpdateUser = 1;
            entityUpdate.AuditUpdateDate = DateTime.Now;

            var usersToDelete = currentGroupUsers.Where(cgu => !group.Users.Contains(cgu.UserId)).ToList();
            var usersToAdd = group.Users.Where(u => !currentGroupUsers.Any(cgu => cgu.UserId == u)).Select(u => new GroupUsers
            {
                GroupId = group.GroupId,
                UserId = u,
                State = 1,
                AuditCreateUser = 1,
                AuditCreateDate = DateTime.Now
            }).ToList();

            foreach (var user in usersToDelete)
            {
                user.State = 0;
                user.AuditDeleteUser = 1;
                user.AuditDeleteDate = DateTime.Now;
            }

            await _context.GroupUsers.AddRangeAsync(usersToAdd);

            var permissionsToDelete = currentGroupPermissions.Where(cgp => !group.Permissions.Contains(cgp.PermissionId)).ToList();
            var permissionsToAdd = group.Permissions.Where(p => !currentGroupPermissions.Any(cgp => cgp.PermissionId == p))
                .Select(p => new GroupPermission
                {
                    GroupId = group.GroupId,
                    PermissionId = p,
                    State = 1,
                    AuditCreateUser = 1,
                    AuditCreateDate = DateTime.Now
                }).ToList();

            foreach (var permission in permissionsToDelete)
            {
                permission.State = 0;
                permission.AuditDeleteUser = 1;
                permission.AuditDeleteDate = DateTime.Now;
            }

            await _context.GroupPermission.AddRangeAsync(permissionsToAdd);

            await _context.SaveChangesAsync();
        }

    }
}
