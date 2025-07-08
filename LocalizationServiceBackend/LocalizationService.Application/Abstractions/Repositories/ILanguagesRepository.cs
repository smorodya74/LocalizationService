using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ILanguagesRepository
    {
        Task<List<Language>?> GetAllAsync(CancellationToken ct);

        Task<Language> GetByCodeAsync(string code, CancellationToken ct);
        
        Task<string> CreateAsync(Language language, CancellationToken ct);
        
        Task<bool> DeleteAsync(string languageCode);

        Task<bool> ExistsAsync(string languageCode);
    }
}