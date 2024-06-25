using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Filters;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.User;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository(context), IUserRepository
    {
        public async Task CreateAsync(CreateUserDto user, long userId)
        {
            var entity = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdEntidad = user.IdEntidad,
                Password = user.Password,
                Email = user.Email,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = userId
            };

            foreach (long idGroup in user.Groups)
            {
                var groupUser = new GroupUsers()
                {
                    GroupId = idGroup,
                    User = entity,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now,
                    AuditCreateUser = userId
                };
                entity.GroupUsers.Add(groupUser);
            }

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatorResponse<ListUsersResponseDto>> ListUsersAsync(ListUserFilter filter)
        {
            var query = _context.Users.Where(u => u.Id != filter.IdUserQuery
                    && u.IdEntidad == filter.IdEntidad
                    && (u.FirstName.Contains(filter.UserName) || filter.UserName == Utils.EMPTY_STRING)
                    && (u.Email.Contains(filter.UserEmail) || filter.UserEmail == Utils.EMPTY_STRING)
                    && (u.State == filter.State || filter.State == Utils.SIN_FILTRO)
                    && (u.AuditDeleteUser == null && u.AuditDeleteDate == null));

            var totalRecords = await query.Select(u => u.Id).CountAsync();

            var users = await query.Select(u => new ListUsersResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                State = u.State,
                AuditCreateDate = u.AuditCreateDate,
                Group = u.GroupUsers
                .Where(gu => gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteUser == null && gu.AuditDeleteDate == null)
                .Select(g => new GroupListDto
                {
                    GroupId = g.GroupId,
                    Name = g.Group.Name,
                    Description = g.Group.Description,
                    FechaCreacion = g.Group.AuditCreateDate,
                    Permissions = g.Group.GroupPermission
                    .Where(rp => rp.State == Utils.ESTADO_ACTIVO && rp.AuditDeleteUser == null && rp.AuditDeleteDate == null)
                    .Select(p => new GroupPermissionListDto()
                    {
                        Name = p.Permission.Name,
                        Description = p.Permission.Description,
                        PermissionId = p.PermissionId
                    }).ToList(),
                    Users = g.Group.GroupUsers
                    .Where(u => u.State == Utils.ESTADO_ACTIVO && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                    .Select(u => new GroupUsersListDto()
                    {
                        Id = u.UserId,
                        FirstName = u.User.FirstName,
                        LastName = u.User.LastName,
                        Email = u.User.Email
                    }).ToList()
                }).ToList()
            })
            .OrderBy(u => u.FirstName)
            .Skip((filter.NumPage - 1) * filter.Records)
            .Take(filter.Records)
            .ToListAsync();

            var result = new PaginatorResponse<ListUsersResponseDto>
            {
                Data = users,
                TotalRecords = totalRecords,
                CurrentPage = filter.NumPage,
                PageSize = filter.Records,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.Records)
            };

            return result;
        }

        public async Task UpdateAsync(CreateUserDto user, bool updateState, bool updatePassword, long userId)
        {
            var entity = (await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync())!;

            if (updatePassword)
            {
                entity.Password = user.Password;
            }

            if (updateState)
            {
                entity.State = user.State;
            }
            else
            {
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
            }

            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            var currentGroupUsers = await _context.GroupUsers.Where(gu => gu.UserId == entity.Id
                && gu.AuditDeleteDate == null && gu.AuditDeleteUser == null).ToListAsync();

            var groupsToDelete = currentGroupUsers.Where(cgu => !user.Groups.Contains(cgu.GroupId)).ToList();
            var groupsToAdd = user.Groups.Where(g => !currentGroupUsers.Any(cgu => cgu.GroupId == g)).Select(g => new GroupUsers
            {
                GroupId = g,
                UserId = entity.Id,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateUser = userId,
                AuditCreateDate = DateTime.Now
            }).ToList();

            foreach (var group in groupsToDelete)
            {
                group.State = Utils.ESTADO_INACTIVO;
                group.AuditDeleteUser = userId;
                group.AuditDeleteDate = DateTime.Now;
            }

            await _context.GroupUsers.AddRangeAsync(groupsToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task<long> UserExistAsync(string email)
        {
            return await _context.Users.Where(u => u.Email.Equals(email))
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDetailResponseDto> UserByEmailAsync(string email)
        {
            var userDetails = await _context.Users
                .AsNoTracking()
                .Where(u => u.Email.Equals(email) && u.State == 1 && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                .Select(u => new UserDetailResponseDto()
                {
                    UserId = u.Id,
                    Name = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    IdEntidad = u.IdEntidad,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Where(ru => ru.State == Utils.ESTADO_ACTIVO && ru.AuditDeleteUser == null && ru.AuditDeleteDate == null)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Where(gu => gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteUser == null && gu.AuditDeleteDate == null)
                        .Select(gu => gu.Group.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }

        public async Task DeleteAsync(long id, long userId)
        {
            var entity = (await _context.Users.Include(g => g.GroupUsers).FirstOrDefaultAsync(u => u.Id == id))!;

            foreach (var groupUser in entity.GroupUsers)
            {
                groupUser.State = Utils.ESTADO_INACTIVO;
                groupUser.AuditDeleteDate = DateTime.Now;
                groupUser.AuditDeleteUser = userId;
            }

            entity.AuditDeleteUser = userId;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<long> IsUserAdminAsync(string email)
        {
            return await _context.GroupUsers.Where(gu => gu.User.Email == email
                    && gu.GroupId == Utils.GROUP_ADMIN_ID)
                .Select(u => u.UserId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserDetailResponseDto> UserByIdAsync(long id)
        {
            var userDetails = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id && u.State == 1 && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                .Select(u => new UserDetailResponseDto()
                {
                    Name = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    IdEntidad = u.IdEntidad,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Where(ru => ru.State == Utils.ESTADO_ACTIVO && ru.AuditDeleteUser == null && ru.AuditDeleteDate == null)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Where(gu => gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteUser == null && gu.AuditDeleteDate == null)
                        .Select(gu => gu.Group.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }

        public async Task<AuthUserDto> AuthUserByIdAsync(long id)
        {
            var userDetails = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == id && u.State == 1 && u.AuditDeleteUser == null && u.AuditDeleteDate == null)
                .Select(u => new AuthUserDto()
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Permissions = u.GroupUsers
                        .SelectMany(ru => ru.Group.GroupPermission)
                        .Where(ru => ru.State == Utils.ESTADO_ACTIVO && ru.AuditDeleteUser == null && ru.AuditDeleteDate == null)
                        .Select(rp => rp.Permission.Name)
                        .Distinct()
                        .ToList(),
                    GroupNames = u.GroupUsers
                        .Where(gu => gu.State == Utils.ESTADO_ACTIVO && gu.AuditDeleteUser == null && gu.AuditDeleteDate == null)
                        .Select(gu => gu.Group.Name)
                        .ToList()
                }).FirstOrDefaultAsync();

            return userDetails!;
        }
    }
}
