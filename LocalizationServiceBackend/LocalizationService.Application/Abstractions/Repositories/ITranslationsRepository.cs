using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ITranslationsRepository
    {
        Task<List<Translation>?> GetAllAsync();
        Task<List<Translation>?> GetByKeyAsync(LocalizationKey key);
        Task<List<Translation>> SearchByKeyAsync(string query);
        
        Task<string> CreateAsync(LocalizationKey key, Translation translation);
        
        Task<string> UpdateAsync(LocalizationKey key, Translation translation);
        
        Task<bool> DeleteAsync(LocalizationKey key);
    }
}