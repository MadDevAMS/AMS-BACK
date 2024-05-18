using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IEntidadRepository
    {
        Task UpdateAsync(Entidad entidad);
    }
}
