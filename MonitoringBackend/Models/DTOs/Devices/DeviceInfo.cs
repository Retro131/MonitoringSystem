namespace Models.DTOs.Devices;

/// <summary>
/// Информация об устройстве
/// </summary>
public class DeviceInfo
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Дата появления первой записи об устройстве
    /// </summary>
    public DateTime CreatedAt { get; init; }
    
    /// <summary>
    /// Дата последнего использования устройства
    /// </summary>
    public DateTime LastSeenAt { get; set; }
}