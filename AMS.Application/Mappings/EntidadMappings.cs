using AMS.Application.Dtos.Entidad;
using AMS.Application.UseCases.Entidades.Command.CreateEntidad;
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
            CreateMap<CreateEntidadCommand, EntidadRegistroDto>();
        }
    }
}
