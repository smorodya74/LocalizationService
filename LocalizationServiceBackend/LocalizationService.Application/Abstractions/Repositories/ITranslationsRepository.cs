using LocalizationService.Application.Models;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Abstractions.Repositories
{
    public interface ITranslationsRepository
    {
        Task<PagedResult<Translation>> GetPageAsync(int page, int pageSize, string search, CancellationToken ct);

        Task<string> CreateForNewKeyAsync(string keyName, IEnumerable<string> languageCodes, CancellationToken ct = default);
        Task<string> CreateForNewLanguageAsync(string languageCode, IEnumerable<string> allKeys, CancellationToken ct = default);


        Task<string> UpdateAsync(LocalizationKey key, Translation translation, CancellationToken ct);
        
        Task<bool> DeleteByKeyAsync(LocalizationKey key, CancellationToken ct);
        Task<bool> DeleteByLanguageAsync(string languageCode, CancellationToken ct);
    }
}