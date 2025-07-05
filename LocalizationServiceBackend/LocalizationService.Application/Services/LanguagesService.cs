using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Validations;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Services
{
    public class LanguagesService
    {
        private readonly ILanguagesRepository _repository;
        private readonly ILocalizationKeysRepository _keyRepository;
        private readonly ITranslationsRepository _translationsRepository;
        private readonly IValidator<Language> _validator;

        public LanguagesService(
            ILanguagesRepository languagesRepository,
            ILocalizationKeysRepository keyRepository,
            ITranslationsRepository translationsRepository,
            IValidator<Language> languageValidator)
        {
            _repository = languagesRepository;
            _keyRepository = keyRepository;
            _translationsRepository = translationsRepository;
            _validator = languageValidator;

        }

        public async Task<List<Language>?> GetAllLanguages(CancellationToken ct)
        {
            return await _repository.GetAllAsync(ct);
        }

        public async Task<string> CreateLanguage(
            Language langValid,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(langValid, ct);

            var code = await _repository.CreateAsync(langValid, ct);
            var keys = (await _keyRepository.GetAllAsync(ct)).Select(k => k.KeyName);

            await _translationsRepository.CreateForNewLanguageAsync(code, keys, ct);

            return code;
        }

        public async Task<bool> DeleteLangugage(string languageCode, CancellationToken ct)
        {
            await _translationsRepository.DeleteByLanguageAsync(languageCode, ct);

            return await _repository.DeleteAsync(languageCode);
        }
    }
}
