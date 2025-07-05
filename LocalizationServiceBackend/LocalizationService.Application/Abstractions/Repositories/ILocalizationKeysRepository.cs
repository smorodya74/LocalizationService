using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILocalizationKeysRepository
    {
        Task<List<LocalizationKey>?> GetAllAsync(CancellationToken ct);
        Task<List<LocalizationKey>> SearchAsync(string query, CancellationToken ct);
        
        Task<string> CreateAsync(LocalizationKey key, CancellationToken ct);
        
        Task<bool> DeleteAsync(LocalizationKey key, CancellationToken ct);

        Task<bool> ExistsAsync(string localizationKey);
    }
}