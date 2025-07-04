using LocalizationService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class LocalizationKeyConfiguration : IEntityTypeConfiguration<LocalizationKeyEntity>
{
    public void Configure(EntityTypeBuilder<LocalizationKeyEntity> builder)
    {
        builder.HasKey(k => k.KeyName);

        builder.Property(k => k.KeyName)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(k => k.KeyName)
            .IsUnique();
    }
}