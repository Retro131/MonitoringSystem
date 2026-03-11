namespace Models.DTOs.Sessions;

/// <summary>
/// Параметры на удаление записей
/// </summary>
public class DeleteParameters
{
    /// <summary>
    /// Дата, информацию старше которой нужно удалить
    /// </summary>
    public DateTime? CleanupDate { get; set; }
    
    /// <summary>
    /// Идентификатор девайса, у которого нужно удалить сессии
    /// </summary>
    public Guid? DeviceId { get; set; }
    
    /// <summary>
    /// Список сессий, которые нужно удалить
    /// </summary>
    public IEnumerable<Guid>? SessionIds { get; set; }
}