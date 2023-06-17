using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace Forum.Infrastructure.Persistence.Configurations;

public class PostConfigurations : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        ConfigurePostsTable(builder);
        ConfigurePostCommentsTable(builder);
    }

    private static void ConfigurePostCommentsTable(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsMany(p => p.Comments, sb =>
        {
            sb.ToTable("PostComments");

            sb.WithOwner().HasForeignKey("PostId");

            sb.HasKey("Id", "PostId");

            sb.Property(c => c.Id)
                .HasColumnName("CommentId")
                .ValueGeneratedNever()
                .HasConversion(
                p => p.Value,
                p => CommentId.Create(p));

            sb.Property(c => c.Content)
                .HasMaxLength(1000)
                .IsRequired();

            sb.Property(c => c.UserId)
                .HasConversion(p => p.Value,
                    value => UserId.Create(value));
        
        });
        builder.Metadata.FindNavigation(nameof(Post.Comments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigurePostsTable(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(p => p.Value,
            value => PostId.Create(value));

        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Content)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.Likes)
            .HasConversion(p => p.Value,
            value => Likes.Create(value));

        builder.Property(p => p.Dislikes)
            .HasConversion(p => p.Value,
            value => Dislikes.Create(value));

        builder.Property(p => p.AuthorId)
            .HasConversion(p => p.Value,
            value => AuthorId.Create(value));
    }
}
