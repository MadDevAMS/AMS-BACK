using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class GroupRepository(ApplicationDbContext context) : BaseRepository(context), IGroupRepository
    {
        public async Task<GroupByIdDto> GetGroupByIdAsync(long groupId)
        {
            var groupDetail = await _context.Groups
                .Where(c => c.Id == groupId && c.State == 1 && c.AuditDeleteUser == null && c.AuditDeleteDate == null)
                .Select(g => new GroupByIdDto()
                {
                    GroupId = groupId,
                    Name = g.Name,
                    Description = g.Description,
                    Users = g.GroupUsers
                        .Select(u => u.UserId)
                        .ToList(),
                    Permissions = g.GroupPermission
                        .Select(rp => rp.Permission.Id)
                        .ToList(),
                    State = g.State
                }).FirstOrDefaultAsync();

            return groupDetail!;
        }

        public async Task<PaginatorResponse<GroupListDto>> ListGroups(ListGroupFilter filter)
        {
            var query = _context.Groups.Where(u => u.EntidadId == filter.IdEntidad
                        && (u.AuditDeleteUser == null && u.AuditDeleteDate == null));

            var totalRecords = await query.Select(u => u.Id).CountAsync();

            var groups = await query.Select(u => new GroupListDto
            {
                GroupId = u.Id,
                Name = u.Name,
                Description = u.Description,
                FechaCreacion = u.AuditCreateDate
            })
                .OrderBy(u => u.Name)
                .Skip((filter.NumPage - 1) * filter.Records)
                .Take(filter.Records)
                .ToListAsync();

            var result = new PaginatorResponse<GroupListDto>
            {
                Data = groups,
                TotalRecords = totalRecords,
                CurrentPage = filter.NumPage,
                PageSize = filter.Records,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.Records)
            };

            return result;
        }
        public async Task CreateAsync(GroupsDto groupDto)
        {
            var group = new Group()
            {
                Name = groupDto.Name,
                Description = groupDto.Description,
                EntidadId = groupDto.IdEntidad,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = Utils.ESTADO_ACTIVO
            };
            foreach(long idPermission in groupDto.Permissions)
            {
                var groupPermission = new GroupPermission()
                {
                    Group = group,
                    PermissionId = idPermission,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now
                };
                group.GroupPermission.Add(groupPermission);
            }
            foreach (long idUser in groupDto.Users)
            {
                var groupUser = new GroupUsers()
                {
                    Group = group,
                    UserId = idUser,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now
                };
                group.GroupUsers.Add(groupUser);
            }
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(GroupsDto group)
        {
            var entityUpdate = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.GroupId);
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
