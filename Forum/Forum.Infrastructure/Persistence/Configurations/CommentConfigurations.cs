using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Models.Identities;
using Forum.Data.PostAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Forum.Data.Common.Errors.Errors;
using Comment = Forum.Data.CommentAggregate.Comment;

namespace Forum.Infrastructure.Persistence.Configurations;

public class CommentConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        ConfigureCommentsTable(builder);
    }



    private static void ConfigureCommentsTable(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(cid => cid.Value,
            value => CommentId.Create(value));

        builder.Property(c => c.Content)
            .IsRequired();

        builder.Property(c => c.AuthorId)
            .ValueGeneratedNever()
            .HasConversion(caid => caid.Value,
            value => AuthorId.Create(value));

        builder.Property(c => c.PostId)
            .ValueGeneratedNever()
            .HasConversion(cpid => cpid.Value,
            value => PostId.Create(value));
    }
}
