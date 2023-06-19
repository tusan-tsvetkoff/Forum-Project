using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.TagAggregate;
using Forum.Data.TagAggregate.ValueObjects;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Forum.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence
{
    public class ForumDbContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public ForumDbContext(DbContextOptions<ForumDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Author> Author { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder); 
        }
    }
}
