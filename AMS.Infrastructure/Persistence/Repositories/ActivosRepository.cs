using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using AMS.Application.Dtos.Activos;
using AMS.Application.Interfaces.Persistence;
using AMS.Domain.Entities;
using AMS.Infrastructure.Commons.Commons;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AMS.Infrastructure.Persistence.Repositories
{
    public class ActivosRepository(ApplicationDbContext context) : BaseRepository(context), IActivosRepository
    {
        public async Task<BaseResponse<FolderResponseDto>> GetFolderById(long idEntidad)
        {
            var parameters = new Dapper.DynamicParameters();
            parameters.Add("idEntidad", idEntidad);
            var data = await GetAllAsync<FolderProcedureDto>(StoreProcedures.SP_GET_FOLDER_BY_ENTIDAD, parameters);

            var folderResponse = MapToFolderResponseDto(data);

            return new BaseResponse<FolderResponseDto>
            {
                Data = folderResponse,
            };
        }

        private FolderResponseDto MapToFolderResponseDto(IEnumerable<FolderProcedureDto> data)
        {
            var folderResponse = new FolderResponseDto
            {
                EntidadId = data.First().EntidadId,
                EntidadName = data.First().EntidadName,
                Areas = new List<AreaDto>()
            };

            var areaDict = new Dictionary<long, AreaDto>();
            var maquinaDict = new Dictionary<long, MaquinaDto>();
            var componenteDict = new Dictionary<long, ComponenteDto>();
            var puntoMonitoreoDict = new Dictionary<long, PuntoMonitoreoDto>();

            foreach (var item in data)
            {
                if (item.AreaId.HasValue)
                {
                    if (!areaDict.ContainsKey(item.AreaId.Value))
                    {
                        var area = new AreaDto
                        {
                            AreaId = item.AreaId.Value,
                            AreaName = item.AreaName!,
                            SubAreas = new List<AreaDto>(),
                            Maquinas = new List<MaquinaDto>()
                        };

                        if (item.ParentId.HasValue && areaDict.ContainsKey(item.ParentId.Value))
                        {
                            areaDict[item.ParentId.Value].SubAreas.Add(area);
                        }
                        else
                        {
                            folderResponse.Areas.Add(area);
                        }

                        areaDict[item.AreaId.Value] = area;
                    }
                }

                if (item.MaquinaId.HasValue)
                {
                    if (!maquinaDict.ContainsKey(item.MaquinaId.Value))
                    {
                        var maquina = new MaquinaDto
                        {
                            MaquinaId = item.MaquinaId.Value,
                            MaquinaName = item.MaquinaName!,
                            Componentes = new List<ComponenteDto>()
                        };

                        if (item.AreaId.HasValue)
                        {
                            areaDict[item.AreaId.Value].Maquinas.Add(maquina);
                        }

                        maquinaDict[item.MaquinaId.Value] = maquina;
                    }
                }

                if (item.ComponenteId.HasValue)
                {
                    if (!componenteDict.ContainsKey(item.ComponenteId.Value))
                    {
                        var componente = new ComponenteDto
                        {
                            ComponenteId = item.ComponenteId.Value,
                            ComponenteName = item.ComponenteName!,
                            PuntosMoniteros = new List<PuntoMonitoreoDto>()
                        };

                        if (item.MaquinaId.HasValue)
                        {
                            maquinaDict[item.MaquinaId.Value].Componentes.Add(componente);
                        }

                        componenteDict[item.ComponenteId.Value] = componente;
                    }
                }

                if (item.PuntoMonitoreoId.HasValue)
                {
                    if (!puntoMonitoreoDict.ContainsKey(item.PuntoMonitoreoId.Value))
                    {
                        var puntoMonitoreo = new PuntoMonitoreoDto
                        {
                            PuntoMonitoreoId = item.PuntoMonitoreoId.Value,
                            PuntoMonitoreoName = item.PuntoMonitoreoName!,
                            Metricas = new List<MetricaDto>()
                        };

                        if (item.ComponenteId.HasValue)
                        {
                            componenteDict[item.ComponenteId.Value].PuntosMoniteros.Add(puntoMonitoreo);
                        }

                        puntoMonitoreoDict[item.PuntoMonitoreoId.Value] = puntoMonitoreo;
                    }
                }

                if (item.MetricaId.HasValue)
                {
                    var metrica = new MetricaDto
                    {
                        MetricaId = item.MetricaId.Value,
                        MetricaName = item.MetricaName
                    };

                    if (item.PuntoMonitoreoId.HasValue)
                    {
                        puntoMonitoreoDict[item.PuntoMonitoreoId.Value].Metricas.Add(metrica);
                    }
                }
            }

            return folderResponse;
        }


        public async Task CreateAreaAsync(Area area)
        {
            area.State = Utils.ESTADO_ACTIVO;
            area.AuditCreateDate = DateTime.Now;
            area.AuditCreateUser = 1;

            await _context.AddRangeAsync(area);
            await _context.SaveChangesAsync();
        }

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
            punto.AuditCreateUser = Utils.ESTADO_ACTIVO;
            punto.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(punto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAreaAsync(long idArea)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var areas = await _context.Area
                    .Include(a => a.Maquinas)
                        .ThenInclude(m => m.Componentes)
                            .ThenInclude(c => c.PuntosMonitoreo)
                                .ThenInclude(p => p.Metricas)
                    .Where(a => a.Id == idArea)
                    .ToListAsync();

                foreach (var area in areas)
                {
                    foreach (var maquina in area.Maquinas)
                    {
                        foreach (var componente in maquina.Componentes)
                        {
                            foreach (var punto in componente.PuntosMonitoreo)
                            {
                                foreach (var metrica in punto.Metricas)
                                {
                                    metrica.AuditDeleteUser = 1;
                                    metrica.AuditDeleteDate = DateTime.Now;
                                }

                                punto.AuditDeleteUser = 1;
                                punto.AuditDeleteDate = DateTime.Now;
                            }

                            componente.AuditDeleteUser = 1;
                            componente.AuditDeleteDate = DateTime.Now;
                        }

                        maquina.AuditDeleteUser = 1;
                        maquina.AuditDeleteDate = DateTime.Now;
                    }

                    area.AuditDeleteUser = 1;
                    area.AuditDeleteDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteMaquinaAsync(long idMaquina)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var maquinas = await _context.Maquina
                    .Include(m => m.Componentes)
                        .ThenInclude(c => c.PuntosMonitoreo)
                            .ThenInclude(p => p.Metricas)
                    .Where(m => m.Id == idMaquina)
                    .ToListAsync();

                foreach (var maquina in maquinas)
                {
                    foreach (var componente in maquina.Componentes)
                    {
                        foreach (var punto in componente.PuntosMonitoreo)
                        {
                            foreach (var metrica in punto.Metricas)
                            {
                                metrica.AuditDeleteUser = 1;
                                metrica.AuditDeleteDate = DateTime.Now;
                            }

                            punto.AuditDeleteUser = 1;
                            punto.AuditDeleteDate = DateTime.Now;
                        }

                        componente.AuditDeleteUser = 1;
                        componente.AuditDeleteDate = DateTime.Now;
                    }

                    maquina.AuditDeleteUser = 1;
                    maquina.AuditDeleteDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task DeleteMetricaAsync(long idMetrica)
        {
            var entity = await _context.Metrica.FindAsync(idMetrica);
            if (entity == null)
            {
                throw new Exception("Metrica not found");
            }

            entity.AuditDeleteUser = 1;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePuntoMonitoreoAsync(long idPunto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var puntos = await _context.PuntoMonitoreo
                    .Include(p => p.Metricas)
                    .Where(p => p.Id == idPunto)
                    .ToListAsync();

                foreach (var punto in puntos)
                {
                    foreach (var metrica in punto.Metricas)
                    {
                        metrica.AuditDeleteUser = 1;
                        metrica.AuditDeleteDate = DateTime.Now;
                    }

                    punto.AuditDeleteUser = 1;
                    punto.AuditDeleteDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteComponenteAsync(long idComponente)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var componentes = await _context.Componente
                    .Include(c => c.PuntosMonitoreo)
                        .ThenInclude(p => p.Metricas)
                    .Where(c => c.Id == idComponente)
                    .ToListAsync();

                foreach (var componente in componentes)
                {
                    foreach (var punto in componente.PuntosMonitoreo)
                    {
                        foreach (var metrica in punto.Metricas)
                        {
                            metrica.AuditDeleteUser = 1;
                            metrica.AuditDeleteDate = DateTime.Now;
                        }

                        punto.AuditDeleteUser = 1;
                        punto.AuditDeleteDate = DateTime.Now;
                    }

                    componente.AuditDeleteUser = 1;
                    componente.AuditDeleteDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<AreaResponseDto> GetAreaByIdAsync(long idArea)
        {
            var entity = (await _context.Area.FirstOrDefaultAsync(c => c.Id == idArea))!;

            var response = new AreaResponseDto()
            {
                Name = entity.Name,
                Description = entity.Description,

            };

            return response;
        }

        public async Task<ComponenteResponseDto> GetComponenteByIdAsync(long idComponente)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(c => c.Id == idComponente))!;

            var response = new ComponenteResponseDto()
            {
                IdComponente = entity.Id,
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
                IdMaquina = entity.Id,
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
                IdMetrica = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tipo = entity.Tipo,
            };

            return metricaDto;
        }

        public async Task<PuntoMonitoreoResponseDto> GetPuntoMonitoreoByIdAsync(long idPunto)
        {
            var entity = (await _context.PuntoMonitoreo.FirstOrDefaultAsync(p => p.Id == idPunto))!;

            var response = new PuntoMonitoreoResponseDto()
            {
                IdPuntoMonitoreo = entity.Id,
                Description = entity.Description,
                Detail = entity.Detail,
                DireccionMedicion = entity.DireccionMedicion,
                AnguloDireccion = entity.AnguloDireccion,
                DatosMedicion = entity.DatosMedicion,
            };

            return response;
        }

        public async Task UpdateAreaAsync(Area area)
        {
            var entity = (await _context.Area.FirstOrDefaultAsync(m => m.Id == area.Id))!;

            entity.Name = area.Name;
            entity.Description = area.Description;
            entity.AuditUpdateUser = Utils.ESTADO_ACTIVO;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
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

        public async Task UpdateMaquinaAsync(Maquina maquina)
        {
            var entity = (await _context.Maquina.FirstOrDefaultAsync(m => m.Id == maquina.Id))!;

            entity.Name = maquina.Name;
            entity.Description = maquina.Description;
            entity.TipoMaquina = maquina.TipoMaquina;
            entity.AuditUpdateUser = Utils.ESTADO_ACTIVO;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

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
            var entityUpdate = (await _context.PuntoMonitoreo.FirstOrDefaultAsync(p => p.Id == punto.Id))!;

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
