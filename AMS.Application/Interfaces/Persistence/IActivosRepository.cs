using AMS.Application.Dtos.Activos;
using AMS.Domain.Entities;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IActivosRepository
    {
        Task CreateMetricasAsync(Metrica metrica);
        Task UpdateMetricasAsync(Metrica metrica);
        Task<MetricasResponseDto> GetMetricaByIdAsync(long idMetrica);
        Task DeleteMetricaAsync(long idMetrica);
        Task CreatePuntoMonitoreoAsync(PuntoMonitoreo punto);
        Task UpdatePuntoMonitorioAsync(PuntoMonitoreo punto);
        Task<PuntoMonitoreoResponseDto> GetPuntoMonitoreoByIdAsync(long idPunto);
        Task DeletePuntoMonitoreoAsync(long idPunto);
        Task<ComponenteResponseDto> GetComponenteByIdAsync(long idComponente);
        Task CreateComponenteAsync(Componente componente);
        Task UpdateComponenteAsync(Componente componente);
        Task DeleteComponenteAsync(long idComponente);
        Task<MaquinaResponseDto> GetMaquinaByIdAsync(long idMaquina);
        Task CreateMaquinaAsync(Maquina maquina);
        Task UpdateMaquinaAsync(Maquina maquina);
        Task DeleteMaquinaAsync(long idMaquina);
    }
}
