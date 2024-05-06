using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistance.Models.Configurations;

public sealed class AuthorConfiguration : IEntityTypeConfiguration<Author.Author>
{
    public void Configure(EntityTypeBuilder<Author.Author> builder)
    {
    }
}