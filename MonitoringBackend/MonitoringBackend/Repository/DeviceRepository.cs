using InfotecsBackend.EF;
using InfotecsBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Pagination;
using Models.Entities;

#pragma warning disable CS1591

namespace InfotecsBackend.Repository;

public class DeviceRepository(SessionsDbContext dbContext) : IDeviceRepository
{
    public async Task<Device?> GetDeviceAsync(Guid sessionInfoDeviceId, CancellationToken token)
    {
        return await dbContext.Devices.FirstOrDefaultAsync(x => x.Id == sessionInfoDeviceId, cancellationToken: token);
    }

    public async Task AddDeviceAsync(Device device, CancellationToken token)
    {
        await dbContext.Devices.AddAsync(device, token);
    }

    public async Task<Page<Device>> GetDevicesAsync(PaginationFilter paginationFilter, CancellationToken token)
    {
        var query = dbContext.Devices.AsNoTracking();

        var totalCount = await query.CountAsync(token);

        query = paginationFilter.SortDirection == SortDirection.Asc
            ? query.OrderBy(x => x.LastSeenAt)
            : query.OrderByDescending(x => x.LastSeenAt);

        var items = await query
            .Skip(paginationFilter.Offset)
            .Take(paginationFilter.Limit)
            .ToListAsync(token);
        
        return new Page<Device>(items, totalCount);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}