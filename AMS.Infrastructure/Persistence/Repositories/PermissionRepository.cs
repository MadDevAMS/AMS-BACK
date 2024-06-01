using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Roles;
using AMS.Application.Interfaces.Persistence;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository(ApplicationDbContext context) : IPermissionRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<PaginatorResponse<PermissionsListResponseDto>> ListPermissionAsync(ListPermissionFilter filter)
        {
            var query = _context.Permissions.Where(u => u.PermissionId == filter.IdPermission
                        && (u.Name.Contains(filter.PermissionName) || filter.PermissionName == Utils.EMPTY_STRING)
                        && (u.State == filter.PermissionState || filter.PermissionState == -1));

            var totalRecords = await query.Select(u => u.PermissionId).CountAsync();

            var permissions = await query.Select(u => new PermissionsListResponseDto
            {
                Id = u.PermissionId,
                Name = u.Name,
                State = filter.PermissionState
            })
             // No puedo exportar por alguna razón 
            .OrderBy(u => u.Name)
            .Skip((filter.NumPage -1 ) * filter.Records) 
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
