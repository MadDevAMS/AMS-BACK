using System.ComponentModel;
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
using AMS.Domain.Entities;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class ActivosMapping : Profile
    {
        public ActivosMapping()
        {
            CreateMap<CreateMetricasCommand, Metrica>();
            CreateMap<UpdateMetricasCommand, Metrica>();

            CreateMap<CreatePuntoMonitoreoCommand, PuntoMonitoreo>();
            CreateMap<UpdatePuntoMonitoreoCommand, PuntoMonitoreo>();

            CreateMap<CreateComponenteCommand, Componente>();
            CreateMap<UpdateComponenteCommand, Componente>();

            CreateMap<CreateMaquinaCommand, Maquina>();
            CreateMap<UpdateMaquinaCommand, Maquina>();

            CreateMap<CreateAreasCommand, Area>();
            CreateMap<UpdateAreasCommand, Area>();

        }
    }
}
