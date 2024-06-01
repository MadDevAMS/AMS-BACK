using AMS.Application.Dtos.Entidad;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class EntidadRepository(ApplicationDbContext context) : IEntidadRepository
    {
        private readonly ApplicationDbContext _context = context;

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
                    Direccion = entidadDto.Direccion,
                    Image = Utils.DEFAULT_IMAGE,
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


        public async Task UpdateAsync(Entidad entidad)
        {
            var entity = (await _context.Entidad.Where(e => e.Id == entidad.Id).FirstOrDefaultAsync())!;

            entity.Image = entidad.Image;
            entity.Email = entidad.Email;
            entity.Direccion = entidad.Direccion;
            entity.Telefono = entidad.Telefono;
            entity.AuditUpdateUser= Utils.ESTADO_ACTIVO;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
