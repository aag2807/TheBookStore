using Core.Boundaries.Persistance.Util;

namespace Core.Boundaries.Persistance;

public interface IBookRepository
{
    /// <summary>
    /// Persist an existing book to the database
    /// </summary>
    /// <param name="book">A valid instance of <see cref="Core.Books"/></param>
    public Task<Core.Books.Book> AddBook(Core.Books.Book book);

    /// <summary>
    /// Retrieves a book from the database
    /// </summary>
    /// <param name="book">A valid integer primitive representing the book's id</param>
    public Task<Core.Books.Book> GetBook(Core.Books.Book book);
}