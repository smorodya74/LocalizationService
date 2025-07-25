﻿using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL.Entities;
using LocalizationService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL.Repositories
{
    public class LocalizationKeysRepository : ILocalizationKeysRepository
    {
        private readonly LocalizationServiceDbContext _context;
        public LocalizationKeysRepository(LocalizationServiceDbContext context) => _context = context;

        public async Task<List<LocalizationKey>?> GetAllAsync(CancellationToken ct)
        {
            var keysEntities = await _context.LocalizationKeys
                .AsNoTracking()
                .ToListAsync(ct);

            var keys = new List<LocalizationKey>();

            foreach (var entity in keysEntities)
            {
                var (key, _) = LocalizationKey.CreateDB(entity.KeyName);
                if (key != null) keys.Add((LocalizationKey)key);
            }

            return keys;
        }

        public async Task<string> CreateAsync(LocalizationKey key, CancellationToken ct)
        {
            var entity = new LocalizationKeyEntity { KeyName = key.KeyName };

            await _context.LocalizationKeys.AddAsync(entity, ct);
            await _context.SaveChangesAsync();

            return key.KeyName;
        }

        public async Task<bool> DeleteAsync(LocalizationKey key, CancellationToken ct)
        {
            var lkey = await _context.LocalizationKeys.FindAsync(key.KeyName);
            if (lkey == null) return false;

            _context.LocalizationKeys.Remove(lkey);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ExistsAsync(string localizationKey)
        {
            return await _context.LocalizationKeys
                .AsNoTracking()
                .AnyAsync(k => k.KeyName == localizationKey);
        }
    }
}