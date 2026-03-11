namespace Models.DTOs.Pagination;

/// <summary>
/// Страница для пагинации
/// </summary>
/// <typeparam name="T">Элементы, которые будут в странице</typeparam>
public class Page<T>(ICollection<T> items, int totalCount)
{
    /// <summary>
    /// Элементы страницы
    /// </summary>
    public IEnumerable<T> Items { get; set; } = items;

    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    public int TotalCount { get; set; } = totalCount;
}