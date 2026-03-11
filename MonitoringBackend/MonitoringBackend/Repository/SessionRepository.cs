using InfotecsBackend.EF;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Pagination;
using Models.DTOs.Sessions;
using Models.Entities;

#pragma warning disable CS1591

namespace InfotecsBackend.Repository;

public class SessionRepository(SessionsDbContext dbContext) : ISessionRepository
{
    public async Task AddSessionAsync(Session session, CancellationToken token)
    {
        await dbContext.Sessions.AddAsync(session, token);
    }

    public async Task<Page<Session>> GetSessionsByDeviceIdAsync(Guid deviceId, PaginationFilter paginationFilter,
        CancellationToken token)
    {
        var query = dbContext.Sessions
            .Where(x => x.DeviceId == deviceId && !x.IsDeleted)
            .AsNoTracking();

        var totalCount = await query.CountAsync(token);

        query = paginationFilter.SortDirection == SortDirection.Asc
            ? query.OrderBy(x => x.StartTime)
            : query.OrderByDescending(x => x.StartTime);
        
        var items = await query
            .Skip(paginationFilter.Offset)
            .Take(paginationFilter.Limit)
            .ToListAsync(token);
        
        return new Page<Session>(items, totalCount);       
    }

    public async Task SoftDeleteSessionsAsync(DeleteParameters deleteParameters, CancellationToken token)
    {
        var query = dbContext.Sessions.AsQueryable();
        
        var hasFilter = false;

        if (deleteParameters.CleanupDate.HasValue)
        {
            query = query.Where(x => x.EndTime < deleteParameters.CleanupDate.Value);
            hasFilter = true;
        }

        if (deleteParameters.DeviceId.HasValue)
        {
            query = query.Where(x => x.DeviceId == deleteParameters.DeviceId.Value);
            hasFilter = true;
        }

        if (deleteParameters.SessionIds != null && deleteParameters.SessionIds.Any())
        {
            query = query.Where(x => deleteParameters.SessionIds.Contains(x.Id));
            hasFilter = true;
        }
        
        if (!hasFilter)
        {
            return;
        }
        
        await query.ExecuteUpdateAsync(s => s
            .SetProperty(x => x.IsDeleted, true), token);
    }
}