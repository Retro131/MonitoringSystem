using InfotecsBackend.EF;
using Models.DTOs.Pagination;
using Models.DTOs.Sessions;
using Models.Entities;

namespace InfotecsBackend.Repository;

public interface ISessionRepository
{
    Task AddSessionAsync(Session session, CancellationToken token);
    Task<Page<Session>> GetSessionsByDeviceIdAsync(Guid deviceId, PaginationFilter paginationFilter, CancellationToken token);
    Task SoftDeleteSessionsAsync(DeleteParameters deleteParameters, CancellationToken token);
}