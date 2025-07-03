using LocalizationService.DAL.Entities;
using LocalizationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LocalizationService.DAL
{
    public class LocalizationServiceDbContext : DbContext
    {
        public LocalizationServiceDbContext(DbContextOptions<LocalizationServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<LanguageEntity> Languages => Set<LanguageEntity>();
        public DbSet<TranslationEntity> Translations => Set<TranslationEntity>();
        public DbSet<LocalizationKeyEntity> LocalizationKeys => Set<LocalizationKeyEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalizationServiceDbContext).Assembly);
        }
    }
}
