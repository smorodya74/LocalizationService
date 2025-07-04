using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Services
{
    public class LocalizationKeysService
    {
        private readonly ILocalizationKeysRepository _repository;
        private readonly IValidator<LocalizationKey> _validator;

        public LocalizationKeysService(
            ILocalizationKeysRepository keysRepository,
            IValidator<LocalizationKey> keyValidator)
        {
            _repository = keysRepository;
            _validator = keyValidator;
        }


        public async Task<List<LocalizationKey>?> GetAllKeys(CancellationToken ct)
        {
            return await _repository.GetAllAsync(ct);
        }

        public async Task<List<LocalizationKey>> SearchKey(
            string querry,
            CancellationToken ct = default)
        {
            return await _repository.SearchAsync(querry);
        }

        public async Task<string> CreateKey(
            LocalizationKey keyValid,
            CancellationToken ct = default)
        {
            await _validator.ValidateAndThrowAsync(keyValid, ct);

            return await _repository.CreateAsync(keyValid);
        }

        public async Task<bool> DeleteKey(LocalizationKey key)
        {
            return await _repository.DeleteAsync(key);
        }
    }
}
