using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class TranslationsRepository : ITranslationsRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public TranslationsRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<List<Translation>?> GetAllAsync(CancellationToken ct)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Include(t => t.Language)
                .ToListAsync(ct);

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

        public async Task<List<Translation>> SearchByKeyAsync(string query, CancellationToken ct)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Include(t => t.Language)
                .Where(t => EF.Functions.ILike(t.LocalizationKey, $"%{query}%"))
                .ToListAsync(ct);

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

        public async Task<List<Translation>?> GetByKeyAsync(LocalizationKey key, CancellationToken ct)
        {
            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Where(t => t.LocalizationKey == key.KeyName)
                .Include(t => t.Language)
                .ToListAsync(ct);

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

        public async Task<string> UpdateAsync(LocalizationKey key, Translation translation, CancellationToken ct)
        {
            var translationEntity = await _context.Translations.FindAsync(
                key.KeyName,
                translation.Language.LanguageCode,
                ct);

            if (translationEntity != null)
            {
                translationEntity.TranslationText = translation.TranslationText ?? string.Empty;
                await _context.SaveChangesAsync(ct);
            }

            return key.KeyName;
        }

        public async Task<bool> DeleteByKeyAsync(LocalizationKey key, CancellationToken ct)
        {
            var translationEntities = await _context.Translations
                .Where(t => t.LocalizationKey == key.KeyName)
                .ToListAsync(ct);

            if (translationEntities.Count == 0)
                return false;

            _context.Translations.RemoveRange(translationEntities);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteByLanguageAsync(string languageCode, CancellationToken ct)
        {
            var translationEntities = await _context.Translations
                .Where(t => t.LanguageCode == languageCode)
                .ToListAsync(ct);

            if (translationEntities.Count == 0)
                return false;

            _context.Translations.RemoveRange(translationEntities);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> CreateForNewKeyAsync(
            string keyName,
            IEnumerable<string> languageCodes,
            CancellationToken ct = default)
        {
            var entities = languageCodes.Select(code => new TranslationEntity
            {
                LocalizationKey = keyName,
                LanguageCode = code,
                TranslationText = string.Empty
            });

            await _context.Translations.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);

            return keyName;
        }

        public async Task<string> CreateForNewLanguageAsync(
            string languageCode,
            IEnumerable<string> allKeys,
            CancellationToken ct = default)
        {
            var entities = allKeys.Select(key => new TranslationEntity
            {
                LocalizationKey = key,
                LanguageCode = languageCode,
                TranslationText = string.Empty
            });

            await _context.Translations.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);

            return languageCode;
        }

    }
}