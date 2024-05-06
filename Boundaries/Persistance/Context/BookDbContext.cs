using Boundaries.Persistance.Models.Author;
using Boundaries.Persistance.Models.Book;
using Boundaries.Persistance.Models.Category;
using Boundaries.Persistance.Models.Customer;
using Boundaries.Persistance.Models.Order;
using Boundaries.Persistance.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Triplex.Validations;

namespace Boundaries.Persistance.Context;

/// <summary>
/// Application DbContext that storage and manage all data.
/// </summary>
public class BookDbContext : DbContext, IBookDbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Arguments.NotNull(optionsBuilder, nameof(optionsBuilder));

        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored, CoreEventId.NavigationBaseIncluded));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Arguments.NotNull(modelBuilder, nameof(modelBuilder));

        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookCategory>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });
        
        SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void SeedData(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasData(
                new User
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
                new User()
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

    /// <inheritdoc/>
    DbSet<Author> IBookDbContext.Author { get; set; }

    /// <inheritdoc/>
    DbSet<Book> IBookDbContext.Book { get; set; }

    /// <inheritdoc/>
    DbSet<BookCategory> IBookDbContext.BookCategory { get; set; }

    /// <inheritdoc/>
    DbSet<BookAuthor> IBookDbContext.BookAuthor { get; set; }

    /// <inheritdoc/>
    DbSet<Category> IBookDbContext.Category { get; set; }

    /// <inheritdoc/>
    DbSet<Customer> IBookDbContext.Customer { get; set; }

    /// <inheritdoc/>
    DbSet<Order> IBookDbContext.Order { get; set; }

    /// <inheritdoc/>
    DbSet<OrderDetail> IBookDbContext.OrderDetail { get; set; }

    /// <inheritdoc/>
    DbSet<User> IBookDbContext.User { get; set; }
 
    /// <inheritdoc cref="IBookDbContext.SaveChanges()"/>
    int IBookDbContext.SaveChanges() => SaveChanges();

    /// <inheritdoc cref="IBookDbContext.SaveChangesAsync()"/>
    async Task<int> IBookDbContext.SaveChangesAsync() => await SaveChangesAsync().ConfigureAwait(true);

    /// <inheritdoc/>
    EntityEntry IBookDbContext.Update<TEntity>(TEntity entity) => Update(entity);

    /// <inheritdoc/>
    void IBookDbContext.RemoveRange(params object[] entities) => RemoveRange(entities);
    
    void IBookDbContext.AddRange<TEntity>(IEnumerable<TEntity> entities) => AddRange(entities);

    /// <inheritdoc cref="IBookDbContext.AddAsync{TEntity}(TEntity)"/>
    async Task<EntityEntry> IBookDbContext.AddAsync<TEntity>(TEntity entity) => await AddAsync(entity).ConfigureAwait(true);
}