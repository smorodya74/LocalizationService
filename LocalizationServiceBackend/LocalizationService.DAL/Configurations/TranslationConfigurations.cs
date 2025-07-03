using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationService.DAL.Configurations
{
    public class TranslationConfigurations : IEntityTypeConfiguration<TranslationEntity>
    {
        public void Configure(EntityTypeBuilder<TranslationEntity> builder)
        {
            builder.HasKey(t => new { t.LocalizationKey, t.LanguageCode });

            builder.Property(t => t.TranslationText)
                .HasColumnType("text")
                .IsRequired(false);

            builder.HasOne(t => t.Language)
                .WithMany(l => l.Translations)
                .HasForeignKey(t => t.LanguageCode)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Localization)
                .WithMany(k => k.Translations)
                .HasForeignKey(t => t.LocalizationKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}