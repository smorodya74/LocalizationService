using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Validations;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Services
{
    public class LanguagesService
    {
        private readonly ILanguagesRepository _repository;
        private readonly IValidator<Language> _validator;

        public LanguagesService(
            ILanguagesRepository languagesRepository,
            IValidator<Language> languageValidator)
        {
            _repository = languagesRepository;
            _validator = languageValidator;

        }

        public async Task<List<Language>?> GetAllLanguages()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<string> CreateLanguage(
            Language langValid,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(langValid, ct);

            return await _repository.CreateAsync(langValid);
        }

        public async Task UpdateLanguage(
            Language langValid,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(langValid, ct);
            await _repository.UpdateAsync(langValid);
        }

        public async Task<bool> DeleteLangugage(string languageCode)
        {
            return await _repository.DeleteAsync(languageCode);
        }
    }
}
