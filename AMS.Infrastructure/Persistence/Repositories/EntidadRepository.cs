using AMS.Application.Dtos.Entidad;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class EntidadRepository(ApplicationDbContext context) : BaseRepository(context), IEntidadRepository
    {
        public async Task CreateAsync(EntidadRegistroDto entidadDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entidad = new Entidad()
                {
                    Nombre = entidadDto.Nombre,
                    RazonSocial = entidadDto.RazonSocial,
                    RUC = entidadDto.RUC,
                    Telefono = entidadDto.Telefono,
                    Email = entidadDto.Email,
                    Direccion = Utils.EMPTY_STRING,
                    Image = Utils.EMPTY_STRING,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateUser = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now,
                };

                var user = new User()
                {
                    FirstName = entidadDto.FirstName,
                    LastName = entidadDto.LastName,
                    Email = entidadDto.UserEmail,
                    Password = entidadDto.Password,
                    Entidad = entidad,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateUser = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now,
                };

                var groupAdminId = await _context.Groups
                    .Where(g => g.Id == Utils.GROUP_ADMIN_ID)
                    .Select(g => g.Id)
                    .FirstOrDefaultAsync();

                var groupUser = new GroupUsers()
                {
                    GroupId = groupAdminId,
                    User = user,
                    State = Utils.ESTADO_ACTIVO,
                    AuditCreateUser = Utils.ESTADO_ACTIVO,
                    AuditCreateDate = DateTime.Now,
                };

                user.GroupUsers.Add(groupUser);
                entidad.Users.Add(user);

                await _context.Entidad.AddAsync(entidad);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<long> EntidadExistAsync(string ruc)
        {
            return await _context.Entidad.Where(e => e.RUC == ruc).Select(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<EntidadDto> GetEntidadAsync(long idEntidad)
        {
            var entity = (await _context.Entidad.FirstOrDefaultAsync(e => e.Id == idEntidad))!;

            var response = new EntidadDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                RazonSocial = entity.RazonSocial,
                RUC = entity.RUC,
                Telefono = entity.Telefono,
                Email = entity.Email,
                Direccion = entity.Direccion,
                Image = entity.Image,
                State = Utils.ESTADO_ACTIVO,
            };

            return response;
        }

        public async Task UpdateAsync(Entidad entidad, long userId)
        {
            var entity = (await _context.Entidad.Where(e => e.Id == entidad.Id).FirstOrDefaultAsync())!;

            if (entidad.Image is not null)
            {
                entity.Image = entidad.Image;
            }

            entity.Email = entidad.Email;
            entity.Direccion = entidad.Direccion;
            entity.Telefono = entidad.Telefono;
            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
