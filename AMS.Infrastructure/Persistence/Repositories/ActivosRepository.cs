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
                Areas = new List<AreaFolderDto>()
            };

            var areaDict = new Dictionary<long, AreaFolderDto>();
            var maquinaDict = new Dictionary<long, MaquinaFolderDto>();
            var componenteDict = new Dictionary<long, ComponenteFolderDto>();
            var puntoMonitoreoDict = new Dictionary<long, PuntoMonitoreoFolderDto>();

            foreach (var item in data)
            {
                if (item.AreaId.HasValue)
                {
                    if (!areaDict.ContainsKey(item.AreaId.Value))
                    {
                        var area = new AreaFolderDto
                        {
                            AreaId = item.AreaId.Value,
                            AreaName = item.AreaName!,
                            SubAreas = new List<AreaFolderDto>(),
                            Maquinas = new List<MaquinaFolderDto>()
                        };

                        if (item.ParentAreaId.HasValue && areaDict.ContainsKey(item.ParentAreaId.Value))
                        {
                            areaDict[item.ParentAreaId.Value].SubAreas.Add(area);
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
                        var maquina = new MaquinaFolderDto
                        {
                            MaquinaId = item.MaquinaId.Value,
                            MaquinaName = item.MaquinaName!,
                            Componentes = new List<ComponenteFolderDto>()
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
                        var componente = new ComponenteFolderDto
                        {
                            ComponenteId = item.ComponenteId.Value,
                            ComponenteName = item.ComponenteName!,
                            PuntosMoniteros = new List<PuntoMonitoreoFolderDto>()
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
                        var puntoMonitoreo = new PuntoMonitoreoFolderDto
                        {
                            PuntoMonitoreoId = item.PuntoMonitoreoId.Value,
                            PuntoMonitoreoName = item.PuntoMonitoreoName!,
                            Metricas = new List<MetricaFolderDto>()
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
                    var metrica = new MetricaFolderDto
                    {
                        MetricaId = item.MetricaId.Value,
                        MetricaName = item.MetricaName!
                    };

                    if (item.PuntoMonitoreoId.HasValue)
                    {
                        puntoMonitoreoDict[item.PuntoMonitoreoId.Value].Metricas.Add(metrica);
                    }
                }
            }

            return folderResponse;
        }


        public async Task<AreaDto> CreateAreaAsync(AreaDto area, long userId)
        {
            var entity = new Area
            {
                Name = area.Name,
                Description = area.Description,
                IdParent = area.IdParent,
                IdEntidad = area.IdEntidad,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = userId
            };

            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            area.IdArea = entity.Id;

            return area;
        }

        public async Task<ComponenteDto> CreateComponenteAsync(ComponenteDto componente, long userId)
        {
            var entity = new Componente()
            {
                IdMaquina = componente.IdMaquina,
                Name = componente.Name,
                Description = componente.Description,
                Velocidad = componente.Velocidad,
                Potencia = componente.Potencia,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = userId,
            };
            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            componente.IdComponente = entity.Id;

            return componente;
        }

        public async Task<MaquinaDto> CreateMaquinaAsync(MaquinaDto maquina, long userId)
        {
            var entity = new Maquina()
            {
                IdArea = maquina.IdArea,
                Name = maquina.Name,
                Description = maquina.Description,
                TipoMaquina = maquina.TipoMaquina,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateUser = userId,
                AuditCreateDate = DateTime.Now,
            };

            await _context.AddRangeAsync(entity);
            await _context.SaveChangesAsync();

            maquina.IdMaquina = entity.Id;

            return maquina;
        }

        public async Task<MetricasDto> CreateMetricasAsync(MetricasDto metrica, long userId)
        {
            var entity = new Metrica()
            {
                IdPuntoMonitoreo = metrica.IdPuntoMonitoreo,
                Name = metrica.Name,
                Description = metrica.Description,
                Tipo = metrica.Tipo,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateDate = DateTime.Now,
                AuditCreateUser = userId,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            metrica.IdMetrica = entity.Id;

            return metrica;
        }

        public async Task<PuntoMonitoreoDto> CreatePuntoMonitoreoAsync(PuntoMonitoreoDto punto, long userId)
        {
            var entity = new PuntoMonitoreo()
            {
                IdComponente = punto.IdComponente,
                Description = punto.Description,
                Detail = punto.Detail,
                DireccionMedicion = punto.DireccionMedicion,
                AnguloDireccion = punto.AnguloDireccion,
                DatosMedicion = punto.DatosMedicion,
                State = Utils.ESTADO_ACTIVO,
                AuditCreateUser = userId,
                AuditCreateDate = DateTime.Now,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            punto.IdPuntoMonitoreo = entity.Id;

            return punto;
        }

        public async Task DeleteAreaAsync(long idArea, long userId)
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
                                    metrica.AuditDeleteUser = userId;
                                    metrica.AuditDeleteDate = DateTime.Now;
                                }

                                punto.AuditDeleteUser = userId;
                                punto.AuditDeleteDate = DateTime.Now;
                            }

                            componente.AuditDeleteUser = userId;
                            componente.AuditDeleteDate = DateTime.Now;
                        }

                        maquina.AuditDeleteUser = userId;
                        maquina.AuditDeleteDate = DateTime.Now;
                    }

                    area.AuditDeleteUser = userId;
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

        public async Task DeleteMaquinaAsync(long idMaquina, long userId)
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
                                metrica.AuditDeleteUser = userId;
                                metrica.AuditDeleteDate = DateTime.Now;
                            }

                            punto.AuditDeleteUser = userId;
                            punto.AuditDeleteDate = DateTime.Now;
                        }

                        componente.AuditDeleteUser = userId;
                        componente.AuditDeleteDate = DateTime.Now;
                    }

                    maquina.AuditDeleteUser = userId;
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


        public async Task DeleteMetricaAsync(long idMetrica, long userId)
        {
            var entity = await _context.Metrica.FindAsync(idMetrica);
            if (entity == null)
            {
                throw new Exception("Metrica not found");
            }

            entity.AuditDeleteUser = userId;
            entity.AuditDeleteDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePuntoMonitoreoAsync(long idPunto, long userId)
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
                        metrica.AuditDeleteUser = userId;
                        metrica.AuditDeleteDate = DateTime.Now;
                    }

                    punto.AuditDeleteUser = userId;
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

        public async Task DeleteComponenteAsync(long idComponente, long userId)
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
                            metrica.AuditDeleteUser = userId;
                            metrica.AuditDeleteDate = DateTime.Now;
                        }

                        punto.AuditDeleteUser = userId;
                        punto.AuditDeleteDate = DateTime.Now;
                    }

                    componente.AuditDeleteUser = userId;
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


        public async Task<AreaDto> GetAreaByIdAsync(long idArea)
        {
            var entity = (await _context.Area.FirstOrDefaultAsync(c => c.Id == idArea))!;

            var response = new AreaDto()
            {
                Name = entity.Name,
                Description = entity.Description,
            };

            return response;
        }

        public async Task<ComponenteDto> GetComponenteByIdAsync(long idComponente)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(c => c.Id == idComponente))!;

            var response = new ComponenteDto()
            {
                IdComponente = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Potencia = entity.Potencia,
                Velocidad = entity.Velocidad,
            };

            return response;
        }

        public async Task<MaquinaDto> GetMaquinaByIdAsync(long idMaquina)
        {
            var entity = (await _context.Maquina.FirstOrDefaultAsync(c => c.Id == idMaquina))!;

            var response = new MaquinaDto()
            {
                IdMaquina = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                TipoMaquina = entity.TipoMaquina,
            };

            return response;
        }

        public async Task<MetricasDto> GetMetricaByIdAsync(long idMetrica)
        {
            var entity = (await _context.Metrica.FirstOrDefaultAsync(m => m.Id == idMetrica))!;

            var metricaDto = new MetricasDto()
            {
                IdMetrica = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Tipo = entity.Tipo,
            };

            return metricaDto;
        }

        public async Task<PuntoMonitoreoDto> GetPuntoMonitoreoByIdAsync(long idPunto)
        {
            var entity = (await _context.PuntoMonitoreo.FirstOrDefaultAsync(p => p.Id == idPunto))!;

            var response = new PuntoMonitoreoDto()
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

        public async Task<AreaDto> UpdateAreaAsync(AreaDto area, long userId)
        {
            var entity = (await _context.Area.FirstOrDefaultAsync(m => m.Id == area.IdArea))!;

            entity.Name = area.Name;
            entity.Description = area.Description;
            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return area;
        }

        public async Task<ComponenteDto> UpdateComponenteAsync(ComponenteDto componente, long userId)
        {
            var entity = (await _context.Componente.FirstOrDefaultAsync(m => m.Id == componente.IdComponente))!;

            entity.Name = componente.Name;
            entity.Description = componente.Description;
            entity.Potencia = componente.Potencia;
            entity.Velocidad = componente.Velocidad;
            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return componente;
        }

        public async Task<MaquinaDto> UpdateMaquinaAsync(MaquinaDto maquina, long userId)
        {
            var entity = (await _context.Maquina.FirstOrDefaultAsync(m => m.Id == maquina.IdMaquina))!;

            entity.Name = maquina.Name;
            entity.Description = maquina.Description;
            entity.TipoMaquina = maquina.TipoMaquina;
            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return maquina;
        }

        public async Task<MetricasDto> UpdateMetricasAsync(MetricasDto metrica, long userId)
        {
            var entity = (await _context.Metrica.FirstOrDefaultAsync(m => m.Id == metrica.IdMetrica))!;

            entity.Name = metrica.Name;
            entity.Description = metrica.Description;
            entity.Tipo = metrica.Tipo;
            entity.AuditUpdateUser = userId;
            entity.AuditUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return metrica;
        }

        public async Task<PuntoMonitoreoDto> UpdatePuntoMonitorioAsync(PuntoMonitoreoDto punto, long userId)
        {
            var entityUpdate = (await _context.PuntoMonitoreo.FirstOrDefaultAsync(p => p.Id == punto.IdPuntoMonitoreo))!;

            entityUpdate.Description = punto.Description;
            entityUpdate.Detail = punto.Detail;
            entityUpdate.DireccionMedicion = punto.DireccionMedicion;
            entityUpdate.AnguloDireccion = punto.AnguloDireccion;
            entityUpdate.DatosMedicion = punto.DatosMedicion;
            entityUpdate.AuditUpdateDate = DateTime.Now;
            entityUpdate.AuditUpdateUser = userId;

            await _context.SaveChangesAsync();

            return punto;
        }
    }
}
