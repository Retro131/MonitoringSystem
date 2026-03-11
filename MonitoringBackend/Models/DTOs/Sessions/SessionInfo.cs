using System.ComponentModel.DataAnnotations;
using Semver;

namespace Models.DTOs.Sessions;

/// <summary>
/// Информация о Сессии
/// </summary>
public class SessionInfo : IValidatableObject
{
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Идентификатор устройства, которому принадлежит данная сессия 
    /// </summary>
    [Required]
    public Guid DeviceId { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required, MaxLength(256)]
    public string Name { get; init; } = string.Empty;
    
    /// <summary>
    /// Начало сессии
    /// </summary>
    [Required]
    public DateTime StartTime { get; init; }
    
    /// <summary>
    /// Конец сессии
    /// </summary>
    [Required]
    public DateTime EndTime { get; init; }
    
    /// <summary>
    /// Версия, установленного приложения
    /// </summary>
    [Required]
    public string Version { get; init; } = string.Empty;
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime >= EndTime) 
            yield return new ValidationResult("Время включения должно быть раньше времени выключения");

        if (!SemVersion.TryParse(Version, SemVersionStyles.Any, out _))
            yield return new ValidationResult("Версия должна быть в формате SemVer");
        
    }
}