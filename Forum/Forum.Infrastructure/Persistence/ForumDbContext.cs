using Forum.Data.AuthorAggregate;
using Forum.Data.CommentAggregate;
using Forum.Data.Models;
using Forum.Data.PostAggregate;
using Forum.Data.TagEntity;
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
        public DbSet<Author> Authors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);

            var adminUser = User.Create(
                firstName: "Admin",
                lastName: "User",
                email: "admin4etotochkakom@example.com",
                username: "admin",
                password: "adminskaparola");

            adminUser.PromoteToAdmin();

            var adminAuthor = Author.Create(
                               "Admin",
                               "User",
                               "admin",
                               UserId.Create(adminUser.Id.Value));

            modelBuilder.Entity<Author>().HasData(adminAuthor);
            modelBuilder.Entity<User>().HasData(adminUser);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
