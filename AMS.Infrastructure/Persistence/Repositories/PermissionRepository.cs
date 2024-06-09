using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository(ApplicationDbContext context) : IPermissionRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionQuery filter)
        {
            var query = _context.Permissions.Where(u => u.State == Utils.ESTADO_ACTIVO);

            var totalRecords = await query.Select(u => u.Id).CountAsync();

            var permissions = await query.Select(u => new PermissionsListResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                State = u.State
            })
            .OrderBy(u => u.Name)
            .Skip((filter.NumPage - 1) * filter.Records)
            .Take(filter.Records)
            .ToListAsync();

            var result = new PaginatorResponse<PermissionsListResponseDto>
            {
                Data = permissions,
                TotalRecords = totalRecords,
                CurrentPage = 1,
                PageSize = 1,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)totalRecords)
            };
            return result;
        }
    }
}