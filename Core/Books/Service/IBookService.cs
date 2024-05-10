using Core.Books.Aggregates;

namespace Core.Books.Service;

public interface IBookService
{

    /// <summary>
    /// Creates a new book
    /// </summary>
    /// <param name="book">A core entity of <see cref="Core.Books"/></param>
    public Task AddBook(Book book);
}