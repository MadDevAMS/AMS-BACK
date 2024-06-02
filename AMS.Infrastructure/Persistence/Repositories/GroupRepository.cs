using Microsoft.EntityFrameworkCore;
using AMS.Infrastructure.Persistence.Context;
using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Commons.Commons;

namespace AMS.Infrastructure.Persistence.Repositories;

public class GroupRepository(ApplicationDbContext context) : IGroupRepository
{
    private readonly ApplicationDbContext _context = context;

 

    public async Task DeleteAsync(long groupId)
    {
        var group = (await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId))!;

        group.AuditDeleteUser = Utils.ESTADO_ACTIVO;
        group.AuditDeleteDate = DateTime.Now;
        await _context.SaveChangesAsync();

    }


}