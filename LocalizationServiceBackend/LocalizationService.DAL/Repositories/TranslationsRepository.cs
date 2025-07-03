using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.Models;
using LocalizationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class TranslationsRepository : ITranslationsRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public TranslationsRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<List<Translation>?> GetAllAsync()
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var entity in translationEntities)
            {
                var (translation, _) = Translation.CreateDB(
                    entity.LanguageCode,
                    entity.TranslationText
                );

                if (translation != null) translations.Add(translation);
            }

            return translations;
        }

        public async Task<List<Translation>?> GetByKeyAsync(LocalizationKey key)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Where(t => t.LocalizationKey == key.KeyName)
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var entity in translationEntities)
            {
                var (translation, _) = Translation.CreateDB(
                    entity.LanguageCode,
                    entity.TranslationText);

                if (translation != null) translations.Add(translation);
            }

            return translations;
        }

        public async Task<Translation?> GetByPairAsync(LocalizationKey key, string languageCode)
        {
            var translationEntity = await _context.Translations
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.LocalizationKey == key.KeyName &&
                    t.LanguageCode == languageCode);

            if (translationEntity == null) return null;

            var (translation, _) = Translation.CreateDB(
                translationEntity.LanguageCode, 
                translationEntity.TranslationText);

            return translation;
        }

        public async Task<List<Translation>?> GetByLanguageAsync(string languageCode)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Where(t => t.LanguageCode == languageCode)
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var entity in translationEntities)
            {
                var (translation, _) = Translation.CreateDB(
                    entity.LanguageCode,
                    entity.TranslationText);

                if (translation != null) translations.Add(translation);
            }

            return translations;
        }


        public async Task<string> CreateAsync(LocalizationKey key, Translation translation)
        {
            var existsKey = await _context.LocalizationKeys
                               .AnyAsync(k => k.LocalizationKey == key.KeyName);
            if (!existsKey)
                throw new InvalidOperationException($"Key '{key.KeyName}' does not exist.");

            var entity = new TranslationEntity
            {
                LocalizationKey = key.KeyName,
                LanguageCode = translation.LanguageCode,
                TranslationText = translation.TranslationText
            };

            await _context.Translations.AddAsync(entity);
            await _context.SaveChangesAsync();

            return key.KeyName;
        }

        public async Task<string> UpdateAsync(LocalizationKey key, Translation translation)
        {
            var entity = await _context.Translations.FindAsync(
                key.KeyName,
                translation.LanguageCode);

            if (entity != null)
            {
                entity.TranslationText = translation.TranslationText;
                await _context.SaveChangesAsync();
            }

            return key.KeyName;
        }

        public async Task<bool> DeleteAsync(LocalizationKey key)
        {
            var translationEntities = await _context.Translations
                .Where(t => t.LocalizationKey == key.KeyName)
                .ToListAsync();

            if (translationEntities.Count == 0)
                return false;

            foreach (var entity in translationEntities)
            {
                _context.Translations.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
