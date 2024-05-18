using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class EntidadRepository(ApplicationDbContext context) : IEntidadRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task UpdateAsync(Entidad entidad)
        {
            var entity = (await _context.Entidad.Where(e => e.Id == entidad.Id).FirstOrDefaultAsync())!;

            entity.Image = entidad.Image;
            entity.Email = entidad.Email;
            entity.Direccion = entidad.Direccion;
            entity.Telefono = entidad.Telefono;

            await _context.SaveChangesAsync();
        }
    }
}
