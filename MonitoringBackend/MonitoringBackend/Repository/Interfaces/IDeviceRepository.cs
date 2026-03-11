using Models.DTOs.Pagination;
using Models.Entities;

namespace InfotecsBackend.Repository.Interfaces;

public interface IDeviceRepository
{
    Task<Device?> GetDeviceAsync(Guid sessionInfoDeviceId, CancellationToken token);
    Task AddDeviceAsync(Device device, CancellationToken token);
    Task SaveChanges();
    Task<Page<Device>> GetDevicesAsync(PaginationFilter paginationFilter, CancellationToken token);
}