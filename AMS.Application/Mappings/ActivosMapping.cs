using AMS.Application.Dtos.Activos;
using AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas;
using AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas;
using AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente;
using AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina;
using AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas;
using AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class ActivosMapping : Profile
    {
        public ActivosMapping()
        {
            CreateMap<CreateMetricasCommand, MetricasDto>();
            CreateMap<UpdateMetricasCommand, MetricasDto>();

            CreateMap<CreatePuntoMonitoreoCommand, PuntoMonitoreoDto>();
            CreateMap<UpdatePuntoMonitoreoCommand, PuntoMonitoreoDto>();

            CreateMap<CreateComponenteCommand, ComponenteDto>();
            CreateMap<UpdateComponenteCommand, ComponenteDto>();

            CreateMap<CreateMaquinaCommand, MaquinaDto>();
            CreateMap<UpdateMaquinaCommand, MaquinaDto>();

            CreateMap<CreateAreasCommand, AreaDto>();
            CreateMap<UpdateAreasCommand, AreaDto>();

        }
    }
}
