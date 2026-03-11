using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Pagination;

/// <summary>
/// Фильтр для пагинации
/// </summary>
public class PaginationFilter
{
    /// <summary>
    /// Лимит записей
    /// </summary>
    [Range(1, 1000)]
    public int Limit { get; set; } = 20;
    
    /// <summary>
    /// Смещение записей
    /// </summary>
    [Range(0, int.MaxValue)]
    public int Offset { get; set; } = 0;
    
    /// <summary>
    /// <inheritdoc cref="SortDirection"/>
    /// </summary>
    public SortDirection SortDirection { get; set; } = SortDirection.Desc;
}