using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Models;
using LocalizationService.DAL.DTO.TranslationDTO;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class TranslationsRepository : ITranslationsRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public TranslationsRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<PagedResult<Translation>> GetPageAsync(
            int page, int pageSize, string search, CancellationToken ct)
        {
            var keyQuery = _context.LocalizationKeys.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
                keyQuery = keyQuery
                    .Where(k => EF.Functions.ILike(k.KeyName, $"%{search}%"));

            var totalKeys = await keyQuery.CountAsync(ct);

            var pagedKeys = await keyQuery
                .OrderBy(k => k.KeyName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(k => k.KeyName)
                .ToListAsync(ct);

            var translationEntities = await _context.Translations
                .AsNoTracking()
                .Include(t => t.Language)
                .Where(t => pagedKeys.Contains(t.LocalizationKey))
                .ToListAsync(ct);

            var translations = MapToDomain(translationEntities);

            return new PagedResult<Translation>(translations, totalKeys, page, pageSize);
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

        private static List<Translation> MapToDomain(IEnumerable<TranslationEntity> translationEntities)
        {
            var translations = new List<Translation>();

            foreach (var entity in translationEntities)
            {
                var (key, _) = LocalizationKey.CreateDB(entity.LocalizationKey);
                var (lang, _) = Language.CreateDB(
                                     entity.Language.LanguageCode,
                                     entity.Language.Name);

                if (key is not null && lang is not null)
                {
                    var translation = new Translation(
                        (LocalizationKey)key,
                        lang,
                        entity.TranslationText
                    );

                    translations.Add(translation);
                }
            }
            return translations;
        }
    }
}