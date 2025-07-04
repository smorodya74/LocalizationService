using LocalizationService.Domain.ValueObjects;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILocalizationKeysRepository
    {
        Task<List<LocalizationKey>?> GetAllAsync();
        Task<List<LocalizationKey>> SearchAsync(string query);
        
        Task<string> CreateAsync(LocalizationKey key);
        
        Task<string> UpdateAsync(LocalizationKey key, string newKey);
        
        Task<bool> DeleteAsync(LocalizationKey key);
    }
}