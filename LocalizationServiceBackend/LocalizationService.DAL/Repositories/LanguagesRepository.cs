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

        public async Task<List<Language>?> GetAllAsync()
        {
            var userEntities = await _context.Languages
                .AsNoTracking()
                .ToListAsync();

            var languages = new List<Language>();

            foreach (var userEntity in userEntities)
            {
                var (language, error) = Language.CreateDB(
                    userEntity.LanguageCode,
                    userEntity.Name
                );

                if (language != null)
                {
                    languages.Add(language);
                }
            }
            return languages;
        }

        public async Task<Language?> GetByKeyAsync(string languageCode)
        {
            var languageEntity = await _context.Languages
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.LanguageCode == languageCode);

            if (languageEntity == null) return null;

            var (language, error) = Language.CreateDB(
                languageEntity.LanguageCode,
                languageEntity.Name);

            return language;
        }

        public async Task<Language?> GetByNameAsync(string name)
        {
            var languageEntity = await _context.Languages
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == name);

            if (languageEntity == null) return null;

            var (language, error) = Language.CreateDB(
                languageEntity.LanguageCode,
                languageEntity.Name);

            return language;
        }


        public async Task<string> CreateAsync(Language language)
        {
            var languageEntity = new LanguageEntity
            {
                LanguageCode = language.LanguageCode,
                Name = language.Name
            };

            await _context.Languages.AddAsync(languageEntity);
            await _context.SaveChangesAsync();

            return languageEntity.LanguageCode;
        }

        public async Task UpdateAsync(Language language)
        {
            await _context.Languages
                .Where(l => l.LanguageCode == language.LanguageCode)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(l => l.Name, l => language.Name));
        }

        public async Task<bool> DeleteAsync(string languageCode)
        {
            var languageEntity = await _context.Languages.FindAsync(languageCode);

            if(languageEntity == null) return false;

            _context.Languages.Remove(languageEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
