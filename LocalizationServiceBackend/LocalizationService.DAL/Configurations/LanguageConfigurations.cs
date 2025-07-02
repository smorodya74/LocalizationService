using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationService.DAL.Configurations
{
    public class LanguageConfigurations : IEntityTypeConfiguration<LanguageEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageEntity> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LanguageKey)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(l => l.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasIndex(l => l.LanguageKey)
                .IsUnique();
        }
    }
}
