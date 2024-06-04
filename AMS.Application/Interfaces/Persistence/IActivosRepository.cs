using AMS.Application.Dtos.Activos;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IActivosRepository
    {
        Task<MetricasResponseDto> GetMetricaByIdAsync(long idMetrica);
        Task CreateMetricasAsync(Metrica metrica);
        Task UpdateMetricasAsync(Metrica metrica);
        Task DeleteMetricaAsync(long idMetrica);
        Task<PuntoMonitoreoResponseDto> GetPuntoMonitoreoByIdAsync(long idPunto);
        Task CreatePuntoMonitoreoAsync(PuntoMonitoreo punto);
        Task UpdatePuntoMonitorioAsync(PuntoMonitoreo punto);
        Task DeletePuntoMonitoreoAsync(long idPunto);
        Task<ComponenteResponseDto> GetComponenteByIdAsync(long idComponente);
        Task CreateComponenteAsync(Componente componente);
        Task UpdateComponenteAsync(Componente componente);
        Task DeleteComponenteAsync(long idComponente);
        Task<MaquinaResponseDto> GetMaquinaByIdAsync(long idMaquina);
        Task CreateMaquinaAsync(Maquina maquina);
        Task UpdateMaquinaAsync(Maquina maquina);
        Task DeleteMaquinaAsync(long idMaquina);
        Task<AreaResponseDto> GetAreaByIdAsync(long idArea);
        Task CreateAreaAsync(Area area);
        Task UpdateAreaAsync(Area area);
        Task DeleteAreaAsync(long idArea);
    }
}
