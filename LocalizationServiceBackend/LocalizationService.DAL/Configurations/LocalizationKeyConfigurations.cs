using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalizationService.DAL.Configurations
{
    public class LocalizationKeyConfigurations : IEntityTypeConfiguration<LocalizationKeyEntity>
    {
        public void Configure(EntityTypeBuilder<LocalizationKeyEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.KeyName)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(k => k.KeyName)
                .IsUnique();
        }
    }
}
