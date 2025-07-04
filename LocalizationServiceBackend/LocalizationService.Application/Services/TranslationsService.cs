using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Domain.Models;
using LocalizationService.Domain.ValueObjects;

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

        public async Task<List<Translation>?> GetAllTranslations()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Translation>?> GetTranslationsByKey(LocalizationKey key)
        {
            return await _repository.GetByKeyAsync(key);
        }

        public async Task<List<Translation>> SearchTranslationsByKey(string query)
        {
            return await _repository.SearchByKeyAsync(query);
        }

        public async Task<string> CreateTranslation(
            LocalizationKey key, 
            Translation translation,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(translation, ct);

            return await _repository.CreateAsync(key, translation);
        }

        public async Task<string> UpdateTranslation(
            LocalizationKey key, 
            Translation translation,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(translation, ct);

            return await _repository.UpdateAsync(key, translation);
        }

        public async Task<bool> DeleteTranslation(LocalizationKey key)
        {
            return await _repository.DeleteAsync(key);
        }
    }
}