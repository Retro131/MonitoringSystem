using InfotecsBackend.Service;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Pagination;
using Models.DTOs.Sessions;

namespace InfotecsBackend.Controllers;

/// <summary>
/// Контроллер для сессий
/// </summary>
/// <param name="deviceSessionService"></param>
[ApiController]
[Route("api/v1/sessions")]
public class SessionController(IDeviceSessionService deviceSessionService) : ControllerBase
{
    /// <summary>
    /// Добавить информацию о сессии
    /// </summary>
    /// <param name="sessionInfo"><inheritdoc cref="SessionInfo" /></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> AddSession(
        [FromBody] SessionInfo sessionInfo,
        CancellationToken token
        )
    {
        await deviceSessionService.HandleSessionInfoAsync(sessionInfo, token );
        
        return NoContent();
    }

    /// <summary>
    /// Получить все сессии устройства, по его идентификатору
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="paginationFilter"><inheritdoc cref="PaginationFilter"/></param>
    /// <param name="token"></param>
    /// <returns><inheritdoc cref="Page{T}"/> <inheritdoc cref="SessionInfo"/></returns>
    [HttpGet]
    [Route("{deviceId:guid}")]
    public async Task<ActionResult<Page<SessionInfo>>> GetSessionsByDevice(
        [FromRoute] Guid deviceId,
        [FromQuery] PaginationFilter paginationFilter,
        CancellationToken token)
    {
        var sessions = await deviceSessionService.GetSessionsByDeviceIdAsync(deviceId, paginationFilter, token);
        
        return Ok(sessions);
    }

    /// <summary>
    /// Удалить вручную сессии
    /// </summary>
    /// <param name="deleteParameters"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult> DeleteSessions(
        [FromBody] DeleteParameters deleteParameters,
        CancellationToken token)
    {
        await deviceSessionService.DeleteSessionsAsync(deleteParameters, token);
        
        return NoContent();
    }
}