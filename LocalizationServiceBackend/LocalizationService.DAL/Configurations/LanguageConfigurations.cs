using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationService.DAL.Configurations
{
    public class LanguageConfigurations : IEntityTypeConfiguration<LanguageEntity>
    {
        public void Configure(EntityTypeBuilder<LanguageEntity> builder)
        {
            builder.HasKey(l => l.LanguageCode);

            builder.Property(l => l.LanguageCode)
                .HasMaxLength(3)
                .IsRequired();

            builder.HasIndex(l => l.LanguageCode)
                .IsUnique();

            builder.Property(l => l.Name)
                .HasMaxLength(64)
                .IsRequired();
        }
    }
}
