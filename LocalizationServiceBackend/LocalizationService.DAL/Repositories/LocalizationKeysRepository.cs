using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class LocalizationKeysRepository : ILocalizationKeysRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public LocalizationKeysRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<List<LocalizationKey>?> GetAllAsync()
        {
            var entities = await _context.LocalizationKeys
                .AsNoTracking()
                .ToListAsync();

            var lkeys = new List<LocalizationKey>();

            foreach (var lkey in entities)
                lkeys.Add((LocalizationKey)lkey.LocalizationKey);

            return lkeys;
        }

        public async Task<LocalizationKey?> GetByKeyAsync(string keyName)
        {
            var lkey = await _context.LocalizationKeys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.LocalizationKey == keyName);

            return lkey == null ? null : (LocalizationKey)lkey.LocalizationKey;
        }

        public async Task<string> CreateAsync(LocalizationKey key)
        {
            var entity = new LocalizationKeyEntity { LocalizationKey = key.KeyName };

            await _context.LocalizationKeys.AddAsync(entity);
            await _context.SaveChangesAsync();

            return key.KeyName;
        }

        public async Task<string> UpdateAsync(LocalizationKey key, string newKey)
        {
            var entity = await _context.LocalizationKeys.FindAsync(key.KeyName);

            if (entity != null)
            {
                entity.LocalizationKey = newKey;
                await _context.SaveChangesAsync();
            }

            return newKey;
        }

        public async Task<bool> DeleteAsync(LocalizationKey key)
        {
            var lkey = await _context.LocalizationKeys.FindAsync(key.KeyName);
            if (lkey == null) return false;

            _context.LocalizationKeys.Remove(lkey);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}