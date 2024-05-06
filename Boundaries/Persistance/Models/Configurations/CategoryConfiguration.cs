using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistance.Models.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Boundaries.Persistance.Models.Category.Category>
{
    public void Configure(EntityTypeBuilder<Category.Category> builder)
    {
        builder.HasData(
            new Category.Category()
        );
    }
}