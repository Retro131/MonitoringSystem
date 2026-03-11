using InfotecsBackend.Service;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Devices;
using Models.DTOs.Pagination;

namespace InfotecsBackend.Controllers;

/// <summary>
/// Контроллер для устройств
/// </summary>
/// <param name="deviceSessionService"></param>
[ApiController]
[Route("api/v1/devices")]
public class DeviceController(IDeviceSessionService deviceSessionService) : Controller
{
    
    /// <summary>
    /// Получить данные об устройствах 
    /// </summary>
    /// <param name="paginationFilter"><inheritdoc cref="PaginationFilter"/></param>
    /// <param name="token"></param>
    /// <returns><inheritdoc cref="Page{T}"/> <inheritdoc cref="DeviceInfo"/></returns>
    [HttpGet]
    public async Task<ActionResult<Page<DeviceInfo>>> GetDevices(
        [FromQuery] PaginationFilter paginationFilter,
        CancellationToken token)
    {
        var devices = await deviceSessionService.GetDevicesAsync(paginationFilter, token);
        
        return Ok(devices);
    }
}