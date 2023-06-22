using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Persistence.Configurations;

public class PostConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        ConfigurePostTable(builder);
        ConfigureLikesValueObject(builder);
        ConfigureDislikesValueObject(builder);
        ConfigurePostCommentsTable(builder);
        ConfigurePostTagsTable(builder);
        ConfigurePostIndexes(builder);
    }

    private static void ConfigurePostIndexes(EntityTypeBuilder<Post> builder)
    {
        builder.HasIndex(p => p.Title)
            .HasDatabaseName("IX_Title");
    }

    // Fuck this
    private static void ConfigurePostTagsTable(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsMany(p => p.TagIds, tb =>
        {
            tb.ToTable("PostTagIds");

            tb.WithOwner().HasForeignKey("PostId");

            tb.HasKey("Id");

            tb.Property(t => t.Value)
                .HasColumnName("TagId")
                .ValueGeneratedNever();

        });
        builder.Metadata.FindNavigation(nameof(Post.TagIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    // Jesus Fucking Christ.
    private static void ConfigurePostCommentsTable(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsMany(p => p.CommentIds, cb =>
        {
            cb.ToTable("PostCommentIds");

            cb.WithOwner().HasForeignKey("PostId");

            cb.HasKey("Id");

            cb.Property(c => c.Value)
                .HasColumnName("CommentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Post.CommentIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigurePostTable(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
            value => PostId.Create(value));

        builder.Property(p => p.Title)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Content)
            .HasMaxLength(8192)
            .IsRequired();

        builder.Property(p => p.CreatedDateTime)
            .IsRequired();

        builder.Property(p => p.AuthorId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
            value => AuthorId.Create(value));
    }

    private static void ConfigureLikesValueObject(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsOne(p => p.Likes, lb =>
        {
            lb.Property(l => l.Value)
              .HasDefaultValue(0)
              .HasColumnName("Likes")
              .HasColumnType("int");
        });
    }

    private static void ConfigureDislikesValueObject(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsOne(p => p.Dislikes, db =>
        {
            db.Property(d => d.Value)
              .HasDefaultValue(0)
              .HasColumnName("Dislikes")
              .HasColumnType("int");
        });
    }
}
