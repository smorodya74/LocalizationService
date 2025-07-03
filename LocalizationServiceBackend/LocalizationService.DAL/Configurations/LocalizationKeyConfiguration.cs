using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class LocalizationKeyConfiguration : IEntityTypeConfiguration<LocalizationKeyEntity>
{
    public void Configure(EntityTypeBuilder<LocalizationKeyEntity> builder)
    {
        builder.HasKey(k => k.LocalizationKey);

        builder.Property(k => k.LocalizationKey)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(k => k.LocalizationKey)
            .IsUnique();
    }
}