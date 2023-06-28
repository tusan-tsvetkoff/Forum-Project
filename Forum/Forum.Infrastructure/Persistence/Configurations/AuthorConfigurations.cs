using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Persistence.Configurations;

public class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        ConfigureAuthorsTable(builder);
        ConfigureAuthorPostsTable(builder);
        ConfigureAuthorCommentsTable(builder);
        ConfigureAuthorIndexes(builder);
    }

    private static void ConfigureAuthorIndexes(EntityTypeBuilder<Author> builder)
    {
        builder.HasIndex(a => a.Username)
            .IsUnique()
            .HasDatabaseName("IX_Author_Username");
    }

    private static void ConfigureAuthorCommentsTable(EntityTypeBuilder<Author> builder)
    {
        builder.OwnsMany(a => a.CommentIds, acb =>
        {
            acb.WithOwner().HasForeignKey("AuthorId");

            acb.ToTable("AuthorCommentIds");

            acb.HasKey("Id");

            acb.Property(ac => ac.Value)
                .HasColumnName("AuthorCommentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Author.CommentIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAuthorPostsTable(EntityTypeBuilder<Author> builder)
    {
        builder.OwnsMany(a => a.PostIds, apb =>
        {
            apb.WithOwner().HasForeignKey("AuthorId");

            apb.ToTable("AuthorPostIds");

            apb.HasKey("Id");

            apb.Property(ap => ap.Value)
                .HasColumnName("AuthorPostId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Author.PostIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureAuthorsTable(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => AuthorId.Create(value));

        builder.Property(a => a.FirstName)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(a => a.Username)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(a => a.AvatarUrl)
            .HasMaxLength(256)
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.Property(a => a.UserId)
            .HasConversion(id => id.Value,
            value => UserId.Create(value));
    }
}
