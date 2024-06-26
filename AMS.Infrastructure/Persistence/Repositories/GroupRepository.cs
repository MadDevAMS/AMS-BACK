using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.Permissions;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;
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
                .Where(c => c.Id == groupId && c.State == Utils.ESTADO_ACTIVO && c.AuditDeleteUser == null && c.AuditDeleteDate == null)
                .Select(g => new GroupByIdDto()
                {
                    GroupId = groupId,
                    Name = g.Name,
                    Description = g.Description,
                    Users = g.GroupUsers
                        .Where(u => u.State == Utils.ESTADO_ACTIVO && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                        .Select(u => u.UserId)
                        .ToList(),
                    Permissions = g.GroupPermission
                        .Where(rp => rp.State == Utils.ESTADO_ACTIVO && rp.AuditDeleteUser == null && rp.AuditDeleteDate == null)
                        .Select(rp => rp.Permission.Id)
                        .ToList(),
                    State = g.State
                }).FirstOrDefaultAsync();

            return groupDetail!;
        }

        public async Task<PaginatorResponse<GroupListDto>> ListGroups(ListGroupFilter filter)
        {
            var query = _context.Groups.Where(u => u.IdEntidad == filter.IdEntidad
                        && (u.State == Utils.ESTADO_ACTIVO
                        && u.AuditDeleteUser == null
                        && u.AuditDeleteDate == null));

            var totalRecords = await query.Select(u => u.Id).CountAsync();

            var groups = await query.Select(u => new GroupListDto
            {
                GroupId = u.Id,
                Name = u.Name,
                Description = u.Description,
                FechaCreacion = u.AuditCreateDate,
                Permissions = u.GroupPermission
                    .Where(rp => rp.State == Utils.ESTADO_ACTIVO && rp.AuditDeleteUser == null && rp.AuditDeleteDate == null)
                    .Select(p => new GroupPermissionListDto()
                    {
                        Name = p.Permission.Name,
                        Description = p.Permission.Description,
                        PermissionId = p.PermissionId
                    }).ToList(),
                Users = u.GroupUsers
                    .Where(u => u.State == Utils.ESTADO_ACTIVO && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                    .Select(u => new GroupUsersListDto()
                    {
                        Id = u.UserId,
                        FirstName = u.User.FirstName,
                        LastName = u.User.LastName,
                        Email = u.User.Email
                    }).ToList()
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

        public async Task DeleteAsync(long id, long userId)
        {
            var entity = (await _context.Groups
                    .Include(g => g.GroupUsers)
                    .Include(g => g.GroupPermission)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync())!;

            foreach (var groupPermission in entity.GroupPermission)
            {
                groupPermission.State = Utils.ESTADO_INACTIVO;
                groupPermission.AuditDeleteDate = DateTime.Now;
                groupPermission.AuditDeleteUser = userId;
            }

            foreach (var groupUser in entity.GroupUsers)
            {
                groupUser.State = Utils.ESTADO_INACTIVO;
                groupUser.AuditDeleteDate = DateTime.Now;
                groupUser.AuditDeleteUser = userId;
            }

            entity.State = Utils.ESTADO_INACTIVO;
            entity.AuditDeleteUser = userId;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(GroupsDto groupDto, long userId)
        {
            var group = new Group()
            {
                Name = groupDto.Name,
                Description = groupDto.Description,
                IdEntidad = groupDto.IdEntidad,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = userId,
            };
            foreach (long idPermission in groupDto.Permissions)
            {
                var groupPermission = new GroupPermission()
                {
                    Group = group,
                    PermissionId = idPermission,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now,
                    AuditCreateUser = userId,
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
                    AuditCreateDate = DateTime.Now,
                    AuditCreateUser = userId,
                };
                group.GroupUsers.Add(groupUser);
            }
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GroupsDto group, long userId)
        {
            var entityUpdate = (await _context.Groups
                .Include(g => g.GroupPermission.Where(gu => gu.GroupId == group.GroupId
                    && (gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteDate == null && gu.AuditDeleteUser == null)))
                .Include(g => g.GroupUsers.Where(gu => gu.GroupId == group.GroupId
                    && (gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteDate == null && gu.AuditDeleteUser == null)))
                .FirstOrDefaultAsync(g => g.Id == group.GroupId))!;

            entityUpdate.Name = group.Name;
            entityUpdate.Description = group.Description;
            entityUpdate.State = group.State;
            entityUpdate.AuditUpdateUser = userId;
            entityUpdate.AuditUpdateDate = DateTime.Now;

            var usersToDelete = entityUpdate.GroupUsers.Where(_ => !group.Users.Contains(_.UserId));
            var usersToAdd = group.Users.Where(u => !entityUpdate.GroupUsers.Any(cgu => cgu.UserId == u)).Select(u => new GroupUsers
            {
                GroupId = group.GroupId,
                UserId = u,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateUser = userId,
                AuditCreateDate = DateTime.Now,
            }).ToList();

            foreach (var user in usersToDelete)
            {
                user.State = Utils.ESTADO_INACTIVO;
                user.AuditDeleteUser = userId;
                user.AuditDeleteDate = DateTime.Now;
            }

            await _context.GroupUsers.AddRangeAsync(usersToAdd);

            var permissionsToDelete = entityUpdate.GroupPermission.Where(cgp => !group.Permissions.Contains(cgp.PermissionId)).ToList();
            var permissionsToAdd = group.Permissions.Where(p => !entityUpdate.GroupPermission.Any(cgp => cgp.PermissionId == p))
                .Select(p => new GroupPermission
                {
                    GroupId = group.GroupId,
                    PermissionId = p,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateUser = userId,
                    AuditCreateDate = DateTime.Now
                });

            foreach (var permission in permissionsToDelete)
            {
                permission.State = Utils.ESTADO_INACTIVO;
                permission.AuditDeleteUser = userId;
                permission.AuditDeleteDate = DateTime.Now;
            }

            await _context.GroupPermission.AddRangeAsync(permissionsToAdd);
            await _context.SaveChangesAsync();
        }

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
                CurrentPage = filter.NumPage,
                PageSize = filter.Records,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)totalRecords)
            };
            return result;
        }
    }
}
