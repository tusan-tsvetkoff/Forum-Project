﻿using Forum.Data.TagAggregate;
using Forum.Data.TagAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Persistence.Configurations;

public class TagConfigurations : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        ConfigureTagsTable(builder);
    }

    private static void ConfigureTagsTable(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
            value => TagId.Create(value));

        builder.Property(t => t.Name)
            .HasMaxLength(32)
            .IsRequired();
    }
}
