using LocalizationService.Domain.ValueObjects;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILocalizationKeysRepository
    {
        Task<List<LocalizationKey>?> GetAllAsync();
        Task<LocalizationKey?> GetByKeyAsync(string keyName);

        Task<string> CreateAsync(LocalizationKey key);
        Task<string> UpdateAsync(LocalizationKey key, string newKey);
        Task<bool> DeleteAsync(LocalizationKey key);
    }
}
