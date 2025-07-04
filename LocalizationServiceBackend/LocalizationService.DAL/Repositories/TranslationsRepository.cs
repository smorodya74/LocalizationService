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
                .Include(t => t.Language)
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var translationEntity in translationEntities)
            {
                var (key, _) = LocalizationKey.CreateDB(translationEntity.LocalizationKey);
                var (language, _) = Language.CreateDB(
                    translationEntity.Language.LanguageCode,
                    translationEntity.Language.Name);

                if (key != null && language != null)
                {
                    translations.Add(new Translation((LocalizationKey)key, language, translationEntity.TranslationText));
                }
            }

            return translations;
        }

        public async Task<List<Translation>> SearchByKeyAsync(string query)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Include(t => t.Language)
                .Where(t => EF.Functions.ILike(t.LocalizationKey, $"%{query}%"))
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var entity in translationEntities)
            {
                var (key, _) = LocalizationKey.CreateDB(entity.LocalizationKey);
                var (language, _) = Language.CreateDB(
                                        entity.Language.LanguageCode,
                                        entity.Language.Name);

                if (key != null && language != null)
                {
                    translations.Add(new Translation((LocalizationKey)key, language, entity.TranslationText));
                }
            }

            return translations;
        }

        public async Task<List<Translation>?> GetByKeyAsync(LocalizationKey key)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Where(t => t.LocalizationKey == key.KeyName)
                .Include(t => t.Language)
                .ToListAsync();

            var translations = new List<Translation>();

            foreach (var translationEntity in translationEntities)
            {
                var (language, _) = Language.CreateDB(
                    translationEntity.Language.LanguageCode,
                    translationEntity.Language.Name);

                if (language != null)
                {
                    translations.Add(new Translation(key, language, translationEntity.TranslationText));
                }
            }

            return translations;
        }

        public async Task<string> CreateAsync(LocalizationKey key, Translation translation)
        {
            var translationEntity = new TranslationEntity
            {
                LocalizationKey = key.KeyName,
                LanguageCode = translation.Language.LanguageCode,
                TranslationText = translation.TranslationText ?? string.Empty
            };

            await _context.Translations.AddAsync(translationEntity);
            await _context.SaveChangesAsync();

            return key.KeyName;
        }

        public async Task<string> UpdateAsync(LocalizationKey key, Translation translation)
        {
            var translationEntity = await _context.Translations.FindAsync(
                key.KeyName,
                translation.Language.LanguageCode);

            if (translationEntity != null)
            {
                translationEntity.TranslationText = translation.TranslationText ?? string.Empty;
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

            _context.Translations.RemoveRange(translationEntities);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}