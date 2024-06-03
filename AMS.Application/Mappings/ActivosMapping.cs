using AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas;
using AMS.Domain.Entities;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class ActivosMapping : Profile
    {
        public ActivosMapping()
        {
            CreateMap<CreateMetricasCommand, Metrica>();
        }
    }
}
