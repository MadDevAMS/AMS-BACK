using AMS.Application.Dtos.Entidad;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IEntidadRepository
    {
        Task UpdateAsync(Entidad entidad);
        Task CreateAsync(EntidadRegistroDto entidadDto);
        Task<long> EntidadExistAsync(string ruc);
    }
}
