using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
            ConfigureUserIndexes(builder);
        }

        private static void ConfigureUserIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Email");

            builder.HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Username");

            // Might not be needed/working.
            /*builder.HasIndex(u => u.Id)
                .IsUnique()
                .HasDatabaseName("IX_Id");*/
        }

        private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(u => u.Value,
                value => UserId.Create(value));

            builder.Property(u => u.FirstName)
                .HasMaxLength(32);

            builder.Property(u => u.LastName)
                .HasMaxLength(32);

            builder.Property(u => u.Email);

            builder.Property(u => u.Password);

            builder.Property(u => u.CreatedDate);

            builder.Property(u => u.Username)
                .HasMaxLength(32);

            builder.Property(u => u.About)
                .IsRequired(false)
                .HasMaxLength(256);
        }
    }
}
