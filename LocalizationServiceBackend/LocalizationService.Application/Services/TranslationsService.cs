using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Models;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Services
{
    public class TranslationsService
    {
        private readonly ITranslationsRepository _repository;
        private readonly IValidator<Translation> _validator;

        public TranslationsService(
            ITranslationsRepository translationsRepository,
            IValidator<Translation> translationValidator)
        {
            _repository = translationsRepository;
            _validator = translationValidator;
        }

        public async Task<PagedResult<Translation>> GetTranslationsPBP(int page, int pageSize, CancellationToken ct)
        {
            return await _repository.GetTranslationsPBP(page, pageSize, ct);
        }

        public async Task<List<Translation>?> GetTranslationsByKey(
            LocalizationKey key,
            CancellationToken ct)
        {
            return await _repository.GetByKeyAsync(key, ct);
        }

        public async Task<List<Translation>> SearchTranslationsByKey(string query, CancellationToken ct)
        {
            return await _repository.SearchByKeyAsync(query, ct);
        }

        public async Task<string> UpdateTranslation(
            LocalizationKey key, 
            Translation translation,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(translation, ct);

            return await _repository.UpdateAsync(key, translation, ct);
        }
    }
}