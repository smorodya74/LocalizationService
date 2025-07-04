using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILanguagesRepository
    {
        Task<List<Language>?> GetAllAsync();
        
        Task<string> CreateAsync(Language language);
        
        Task UpdateAsync(Language language);
        
        Task<bool> DeleteAsync(string languageCode);
    }
}