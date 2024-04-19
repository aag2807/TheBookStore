using Boundaries.Persistance.Models.Author;
using Boundaries.Persistance.Models.Book;
using Boundaries.Persistance.Models.Category;
using Boundaries.Persistance.Models.Customer;
using Boundaries.Persistance.Models.Order;
using Boundaries.Persistance.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        base.OnModelCreating(modelBuilder);
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
}