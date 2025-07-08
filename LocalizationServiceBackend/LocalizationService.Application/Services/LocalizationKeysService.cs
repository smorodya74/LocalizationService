using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Services
{
    public class LocalizationKeysService
    {
        private readonly ILocalizationKeysRepository _repository;
        private readonly ILanguagesRepository _langRepository;
        private readonly ITranslationsRepository _translationsRepository;
        private readonly IValidator<LocalizationKey> _validator;

        public LocalizationKeysService(
            ILocalizationKeysRepository keysRepository,
            ILanguagesRepository languagesRepository,
            ITranslationsRepository translationsRepository,
            IValidator<LocalizationKey> keyValidator)
        {
            _repository = keysRepository;
            _langRepository = languagesRepository;
            _translationsRepository = translationsRepository;
            _validator = keyValidator;
        }


        public async Task<List<LocalizationKey>?> GetAllKeys(CancellationToken ct)
        {
            return await _repository.GetAllAsync(ct);
        }

        public async Task<string> CreateKey(
            LocalizationKey keyValid,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(keyValid, ct);

            var keyName = await _repository.CreateAsync(keyValid, ct);
            var languageCodes = (await _langRepository.GetAllAsync(ct))
                .Select(l => l.LanguageCode);

            await _translationsRepository.CreateForNewKeyAsync(keyName, languageCodes, ct);

            return keyName;
        }

        public async Task<bool> DeleteKey(LocalizationKey key, CancellationToken ct)
        {
            await _translationsRepository.DeleteByKeyAsync(key, ct);

            return await _repository.DeleteAsync(key, ct);
        }
    }
}
