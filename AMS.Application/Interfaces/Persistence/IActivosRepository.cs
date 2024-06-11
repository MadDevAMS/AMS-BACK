using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;

namespace AMS.Application.Interfaces.Persistence
{
    public interface IActivosRepository
    {
        Task<BaseResponse<FolderResponseDto>> GetFolderById(long idEntidad);
        Task<MetricasDto> GetMetricaByIdAsync(long idMetrica);
        Task<MetricasDto> CreateMetricasAsync(MetricasDto metrica, long userId);
        Task<MetricasDto> UpdateMetricasAsync(MetricasDto metrica, long userId);
        Task DeleteMetricaAsync(long idMetrica, long userId);
        Task<PuntoMonitoreoDto> GetPuntoMonitoreoByIdAsync(long idPunto);
        Task<PuntoMonitoreoDto> CreatePuntoMonitoreoAsync(PuntoMonitoreoDto punto, long userId);
        Task<PuntoMonitoreoDto> UpdatePuntoMonitorioAsync(PuntoMonitoreoDto punto, long userId);
        Task DeletePuntoMonitoreoAsync(long idPunto, long userId);
        Task<ComponenteDto> GetComponenteByIdAsync(long idComponente);
        Task<ComponenteDto> CreateComponenteAsync(ComponenteDto componente, long userId);
        Task<ComponenteDto> UpdateComponenteAsync(ComponenteDto componente, long userId);
        Task DeleteComponenteAsync(long idComponente, long userId);
        Task<MaquinaDto> GetMaquinaByIdAsync(long idMaquina);
        Task<MaquinaDto> CreateMaquinaAsync(MaquinaDto maquina, long userId);
        Task<MaquinaDto> UpdateMaquinaAsync(MaquinaDto maquina, long userId);
        Task DeleteMaquinaAsync(long idMaquina, long userId);
        Task<AreaDto> GetAreaByIdAsync(long idArea);
        Task<AreaDto> CreateAreaAsync(AreaDto area, long userId);
        Task<AreaDto> UpdateAreaAsync(AreaDto area, long userId);
        Task DeleteAreaAsync(long idArea, long userId);
    }
}
