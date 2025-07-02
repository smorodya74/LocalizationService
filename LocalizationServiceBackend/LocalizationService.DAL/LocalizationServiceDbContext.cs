using LocalizationService.DAL.Entities;
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
        public DbSet<LocalizationKeyEntity> Localizations => Set<LocalizationKeyEntity>();
        public DbSet<TranslationEntity> Translations => Set<TranslationEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalizationServiceDbContext).Assembly);
        }
    }
}
