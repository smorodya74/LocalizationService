namespace LocalizationService.Application.Models
{
    public record PagedResult<T>(
        IReadOnlyList<T> Items,
        int TotalKeys,
        int Page,
        int PageSize);
}
