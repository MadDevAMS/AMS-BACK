using Microsoft.EntityFrameworkCore;
using AMS.Infrastructure.Persistence.Context;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Application.Dtos.Groups;

namespace AMS.Infrastructure.Persistence.Repositories;

public class GroupRepository(ApplicationDbContext context) : IGroupRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateAsync(GroupsDto group)
    {

        var createGroup = new Group
        {
            Name = group.Name,
            Description = group.Description,
            State = group.State,
            AuditCreateUser = Utils.ESTADO_ACTIVO,
            AuditCreateDate = DateTime.Now
        };

        await _context.Groups.AddAsync(createGroup);
        await _context.SaveChangesAsync();

        // aca se menciona los usuarios del grupo, si es que se ingresan claro.
        if (group.Users.Any())
        {
            var groupUsers = group.Users.Select(userId => new GroupUsers
            {
                Groupid = group.GroupId,
                UserId = userId,
                State = 1,
                AuditCreateUser = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now
            });

            await _context.GroupUsers.AddRangeAsync(groupUsers);
        }

        // Agregar permisos al grupo, si se proporcionan claro
        if (group.Permissions.Any())
        {
            var groupPermissions = group.Permissions.Select(permissionId => new GroupPermission
            {
                PermissionId = permissionId,
                GroupId = group.GroupId,
                State = 1,
                AuditCreateUser = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now
            });

            await _context.GroupPermission.AddRangeAsync(groupPermissions);
        }

        await _context.SaveChangesAsync();
    }
    public async Task<long> GroupExistsAsync(string groupName, long idEntidad)
    {
        return await _context.Groups.Where(g => g.Description.Equals(groupName))  // se debe cambiar eso por Name ?
            .Select(g => g.Id)
            .FirstOrDefaultAsync();
    }
}
