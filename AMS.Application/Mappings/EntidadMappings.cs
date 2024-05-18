using AMS.Application.UseCases.Entidades.Command.UpdateEntidad;
using AMS.Domain.Entities;
using AutoMapper;

namespace AMS.Application.Mappings
{
    public class EntidadMappings : Profile
    {
        public EntidadMappings()
        {
            CreateMap<UpdateEntidadCommand, Entidad>();
        }
    }
}
