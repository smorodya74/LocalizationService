using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public LanguagesRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<List<Language>?> GetAllAsync(CancellationToken ct)
        {
            var languageEntities = await _context.Languages
                .AsNoTracking()
                .ToListAsync(ct);

            var languages = new List<Language>();

            foreach (var entity in languageEntities)
            {
                var (lang, _) = Language.CreateDB(entity.LanguageCode, entity.Name);
                if (lang != null) languages.Add(lang);
            }

            return languages;
        }

        public async Task<Language?> GetByCodeAsync(string code, CancellationToken ct = default)
        {
            var languageEntity = await _context.Languages
                .AsNoTracking()
                .SingleOrDefaultAsync(entity => entity.LanguageCode == code, ct);

            return languageEntity is null
                ? null
                : new Language(languageEntity.LanguageCode,
                               languageEntity.Name);
        }

        public async Task<string> CreateAsync(Language language, CancellationToken ct)
        {
            var languageEntity = new LanguageEntity
            {
                LanguageCode = language.LanguageCode,
                Name = language.Name
            };

            await _context.Languages.AddAsync(languageEntity, ct);
            await _context.SaveChangesAsync();

            return languageEntity.LanguageCode;
        }

        public async Task<bool> DeleteAsync(string languageCode)
        {
            var languageEntity = await _context.Languages.FindAsync(languageCode);

            if (languageEntity == null) return false;

            _context.Languages.Remove(languageEntity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(string languageCode)
        {
            return await _context.Languages
                .AsNoTracking()
                .AnyAsync(l => l.LanguageCode == languageCode);
        }
    }
}
