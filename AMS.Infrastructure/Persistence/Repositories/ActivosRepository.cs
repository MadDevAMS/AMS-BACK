using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class ActivosRepository(ApplicationDbContext context) : IActivosRepository
    {

        private readonly ApplicationDbContext _context = context;

        public async Task CreateComponenteAsync(Componente componente)
        {
            componente.State = Utils.ESTADO_ACTIVO;
            componente.AuditCreateDate = DateTime.Now;
            componente.AuditCreateUser = 1;

            await _context.AddRangeAsync(componente);
            await _context.SaveChangesAsync();
        }

        public async Task CreateMaquinaAsync(Maquina maquina)
        {
            maquina.State = Utils.ESTADO_ACTIVO;
            maquina.AuditCreateUser = 1;
            maquina.AuditCreateDate = DateTime.Now;

            await _context.AddRangeAsync(maquina);
            await _context.SaveChangesAsync();
        }

        public async Task CreateMetricasAsync(Metrica metrica)
        {
            metrica.State = Utils.ESTADO_ACTIVO;
            metrica.AuditCreateDate = DateTime.Now;
            metrica.AuditCreateUser = Utils.ESTADO_ACTIVO;

            await _context.AddAsync(metrica);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePuntoMonitoreoAsync(PuntoMonitoreo punto)
        {
            punto.State = Utils.ESTADO_ACTIVO;
            punto.AuditCreateUser = 1;
            punto.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(punto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComponenteAsync(long idComponente)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(c => c.Id == idComponente))!;

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaquinaAsync(long idMaquina)
        {
            var entity = (await _context.Maquina.FirstOrDefaultAsync(c => c.Id == idMaquina))!;

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMetricaAsync(long idMetrica)
        {
            var entity = (await _context.Metrica.FirstOrDefaultAsync(m => m.Id == idMetrica))!;

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePuntoMonitoreoAsync(long idPunto)
        {
            var entity = (await _context.PuntoMonitoreos.FirstOrDefaultAsync(p => p.Id == idPunto))!;

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<ComponenteResponseDto> GetComponenteByIdAsync(long idComponente)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(c => c.Id == idComponente))!;

            var response = new ComponenteResponseDto()
            {
                Name = entity.Name,
                Description = entity.Description,
                Potencia = entity.Potencia,
                Velocidad = entity.Velocidad,

            };

            return response;
        }

        public async Task<MaquinaResponseDto> GetMaquinaByIdAsync(long idMaquina)
        {
            var entity = (await _context.Maquina.FirstOrDefaultAsync(c => c.Id == idMaquina))!;

            var response = new MaquinaResponseDto()
            {
                Name = entity.Name,
                Description = entity.Description,
                TipoMaquina = entity.TipoMaquina,
            };

            return response;
        }

        public async Task<MetricasResponseDto> GetMetricaByIdAsync(long idMetrica)
        {
            var entity = (await _context.Metrica.FirstOrDefaultAsync(m => m.Id == idMetrica))!;

            var metricaDto = new MetricasResponseDto()
            {
                IdPuntoMonitoreo = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tipo = entity.Tipo,
            };

            return metricaDto;
        }

        public async Task<PuntoMonitoreoResponseDto> GetPuntoMonitoreoByIdAsync(long idPunto)
        {
            var entity = (await _context.PuntoMonitoreos.FirstOrDefaultAsync(p => p.Id == idPunto))!;

            var response = new PuntoMonitoreoResponseDto()
            {
                Id = entity.Id,
                Description = entity.Description,
                Detail = entity.Detail,
                DireccionMedicion = entity.DireccionMedicion,
                AnguloDireccion = entity.AnguloDireccion,
                DatosMedicion = entity.DatosMedicion,
            };

            return response;
        }

        public async Task UpdateComponenteAsync(Componente componente)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(m => m.Id == componente.Id))!;

            entity.Name = componente.Name;
            entity.Description = componente.Description;
            entity.Potencia = componente.Potencia;
            entity.Velocidad = componente.Velocidad;
            entity.AuditUpdateUser = Utils.ESTADO_ACTIVO;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public Task UpdateMaquinaAsync(Maquina maquina)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMetricasAsync(Metrica metrica)
        {
            var entity = (await _context.Metrica.FirstOrDefaultAsync(m => m.Id == metrica.Id))!;

            entity.Name = metrica.Name;
            entity.Description = metrica.Description;
            entity.Tipo = metrica.Tipo;
            entity.AuditUpdateUser = Utils.ESTADO_ACTIVO;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePuntoMonitorioAsync(PuntoMonitoreo punto)
        {
            var entityUpdate = (await _context.PuntoMonitoreos.FirstOrDefaultAsync(p => p.Id == punto.Id))!;

            entityUpdate.Description = punto.Description;
            entityUpdate.Detail = punto.Detail;
            entityUpdate.DireccionMedicion = punto.DireccionMedicion;
            entityUpdate.AnguloDireccion = punto.AnguloDireccion;
            entityUpdate.DatosMedicion = punto.DatosMedicion;
            entityUpdate.AuditUpdateDate = DateTime.Now;
            entityUpdate.AuditUpdateUser = 1;

            await _context.SaveChangesAsync();
        }
    }
}
