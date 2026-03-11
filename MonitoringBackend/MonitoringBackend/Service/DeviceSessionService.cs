using InfotecsBackend.Errors.Exceptions;
using InfotecsBackend.Repository;
using InfotecsBackend.Repository.Interfaces;
using Models.DTOs.Devices;
using Models.DTOs.Pagination;
using Models.DTOs.Sessions;
using Models.Entities;
using Models.Mapping;

namespace InfotecsBackend.Service;

public class DeviceSessionService(
    ISessionRepository sessionRepository,
    IDeviceRepository deviceRepository,
    ILogger<DeviceSessionService> logger)
    : IDeviceSessionService
{
    public async Task HandleSessionInfoAsync(SessionInfo sessionInfo, CancellationToken token)
    {
        logger.LogDebug("Начало обработки сессии для DeviceId: {DeviceId}", sessionInfo.DeviceId);

        var device = await deviceRepository.GetDeviceAsync(sessionInfo.DeviceId, token);

        if (device == null)
        {
            logger.LogInformation("Устройство {DeviceId} не найдено. Создаем новое.", sessionInfo.DeviceId);
            device = new Device(sessionInfo.DeviceId);
            await deviceRepository.AddDeviceAsync(device, token);
        }
        else
        {
            device.LastSeenAt = sessionInfo.EndTime > device.LastSeenAt ? sessionInfo.EndTime : device.LastSeenAt;
            
            logger.LogDebug("Обновлено время активности устройства {DeviceId}. Было: {OldTime}, Стало: {NewTime}",
                device.Id, device.LastSeenAt, sessionInfo.EndTime);
        }

        var session = sessionInfo.ToSession();

        await sessionRepository.AddSessionAsync(session, token);

        await deviceRepository.SaveChanges();
        
        logger.LogInformation("Сессия успешно сохранена для DeviceId: {DeviceId}. SessionId: {SessionId}", 
            sessionInfo.DeviceId, session.Id);
    }

    public async Task<Page<DeviceInfo>> GetDevicesAsync(PaginationFilter paginationFilter, CancellationToken token)
    {
        var pagedDevices = await deviceRepository.GetDevicesAsync(paginationFilter, token);

        var devicesInfo = pagedDevices.Items.Select(x => x.ToDeviceInfo()).ToList();

        return new Page<DeviceInfo>(devicesInfo, pagedDevices.TotalCount);
    }

    public async Task<Page<SessionInfo>> GetSessionsByDeviceIdAsync(
        Guid deviceId,
        PaginationFilter paginationFilter,
        CancellationToken token)
    {
        var device = await deviceRepository.GetDeviceAsync(deviceId, token);

        if (device == null)
        {
            logger.LogWarning("Запрос сессий для несуществующего устройства: {DeviceId}", deviceId);
            
            throw new NotFoundException(ErrorMessages.DeviceNotFound(deviceId));
        }

        var sessions = await sessionRepository.GetSessionsByDeviceIdAsync(deviceId, paginationFilter, token);

        var sessionsInfo = sessions.Items.Select(x => x.ToSessionInfo()).ToList();

        return new Page<SessionInfo>(sessionsInfo, sessions.TotalCount);
    }

    public async Task DeleteSessionsAsync(DeleteParameters deleteParameters, CancellationToken token)
    {
        logger.LogInformation("Запущена очистка данных. Критерий даты: {Date}. DeviceId: {DeviceId}", 
            deleteParameters.CleanupDate, deleteParameters.DeviceId);
        await sessionRepository.SoftDeleteSessionsAsync(deleteParameters, token);
    }
}