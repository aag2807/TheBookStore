using Boundaries.Persistance.Models.Author;
using Boundaries.Persistance.Models.Book;
using Boundaries.Persistance.Models.Category;
using Boundaries.Persistance.Models.Customer;
using Boundaries.Persistance.Models.Order;
using Boundaries.Persistance.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Boundaries.Persistance.Context;

/// <summary>
/// The interface for the applications DB context
/// </summary>
public interface IBookDbContext
{
    /// <summary>
    /// Represents the entity <see cref="Author"/> on the DataBase
    /// </summary>
    public DbSet<Author> Author { get; set; }

    /// <summary>
    /// Represents the entity <see cref="Book"/> on the DataBase
    /// </summary>
    public DbSet<Book> Book { get; set; }

    /// <summary>
    /// Represents the entity <see cref="BookCategory"/> on the DataBase
    /// </summary>
    /// 
    public DbSet<BookCategory> BookCategory { get; set; }

    /// <summary>
    /// Represents the entity <see cref="BookAuthor"/> on the DataBase
    /// </summary>
    public DbSet<BookAuthor> BookAuthor { get; set; }

    /// <summary>
    /// Represents the entity <see cref="Category"/> on the DataBase
    /// </summary>
    public DbSet<Category> Category { get; set; }

    /// <summary>
    /// Represents the entity <see cref="Customer"/> on the DataBase
    /// </summary>
    public DbSet<Customer> Customer { get; set; }

    /// <summary>
    /// Represents the entity <see cref="Order"/> on the DataBase
    /// </summary>
    public DbSet<Order> Order { get; set; }

    /// <summary>
    /// Represents the entity <see cref="OrderDetail"/> on the DataBase
    /// </summary>
    public DbSet<OrderDetail> OrderDetail { get; set; }

    /// <summary>
    /// Represents the entity <see cref="User"/> on the DataBase
    /// </summary>
    public DbSet<User> User { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<int> SaveChangesAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int SaveChanges();
}