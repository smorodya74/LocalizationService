using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILanguagesRepository
    {
        Task<List<Language>?> GetAllAsync();
        Task<Language?> GetByKeyAsync(string languageCode);
        Task<Language?> GetByNameAsync(string name);
        
        Task<string> CreateAsync(Language language);
        Task UpdateAsync(Language language);
        Task<bool> DeleteAsync(string languageCode);
    }
}
