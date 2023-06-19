using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.TagAggregate;
using Forum.Data.TagAggregate.ValueObjects;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);
            modelBuilder.Ignore<AuthorId>();
            modelBuilder.Ignore<UserId>();
            modelBuilder.Ignore<TagId>();
            modelBuilder.Ignore<CommentId>();
            modelBuilder.Ignore<PostId>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
