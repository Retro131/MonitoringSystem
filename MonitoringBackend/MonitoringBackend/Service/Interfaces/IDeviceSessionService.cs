using Models.DTOs.Devices;
using Models.DTOs.Pagination;
using Models.DTOs.Sessions;

namespace InfotecsBackend.Service;

public interface IDeviceSessionService
{
    Task HandleSessionInfoAsync(SessionInfo sessionInfo, CancellationToken token);
    
    Task<Page<DeviceInfo>> GetDevicesAsync(PaginationFilter paginationFilter ,CancellationToken token);
    
    Task<Page<SessionInfo>> GetSessionsByDeviceIdAsync(Guid deviceId, PaginationFilter paginationFilter, CancellationToken token);
    
    Task DeleteSessionsAsync(DeleteParameters deleteParameters, CancellationToken token);
}