using LocalizationService.Application.Models;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ITranslationsRepository
    {
        Task<List<Translation>?> GetByKeyAsync(LocalizationKey key, CancellationToken ct);
        Task<List<Translation>> SearchByKeyAsync(string query, CancellationToken ct);
        Task<PagedResult<Translation>> GetTranslationsPBP(int page, int pageSize, CancellationToken ct = default);
        
        Task<string> CreateForNewKeyAsync(string keyName, IEnumerable<string> languageCodes, CancellationToken ct = default);
        Task<string> CreateForNewLanguageAsync(string languageCode, IEnumerable<string> allKeys, CancellationToken ct = default);


        Task<string> UpdateAsync(LocalizationKey key, Translation translation, CancellationToken ct);
        
        Task<bool> DeleteByKeyAsync(LocalizationKey key, CancellationToken ct);
        Task<bool> DeleteByLanguageAsync(string languageCode, CancellationToken ct);
    }
}