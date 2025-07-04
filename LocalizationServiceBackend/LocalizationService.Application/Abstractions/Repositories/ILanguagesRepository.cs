using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILanguagesRepository
    {
        Task<List<Language>?> GetAllAsync(CancellationToken ct);
        
        Task<string> CreateAsync(Language language);
        
        Task<bool> DeleteAsync(string languageCode);

        Task<bool> ExistsAsync(string languageCode);
    }
}