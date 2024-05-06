using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistance.Models.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User.User>
{
    public void Configure(EntityTypeBuilder<User.User> builder)
    {
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<User.User> builder)
    {
        builder.HasData(
            new User.User
            {
                UserId = 1,
                Username = "admin",
                Password = "admin",
                Email = "admin@email.com",
                IsAdmin = true,
                IsBlocked = false,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new User.User()
            {
                UserId = 2,
                Username = "user",
                Password = "user",
                Email = "user@email.com",
                IsAdmin = false,
                IsBlocked = false,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        );
    }
}