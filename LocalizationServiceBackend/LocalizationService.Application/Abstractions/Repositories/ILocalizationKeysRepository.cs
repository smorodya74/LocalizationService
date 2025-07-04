using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILocalizationKeysRepository
    {
        Task<List<LocalizationKey>?> GetAllAsync(CancellationToken ct);
        Task<List<LocalizationKey>> SearchAsync(string query);
        
        Task<string> CreateAsync(LocalizationKey key);
        
        Task<bool> DeleteAsync(LocalizationKey key);

        Task<bool> ExistsAsync(string localizationKey);
    }
}