using Boundaries.Persistance.Models.Book;
using Core.Books.Aggregates;
using Core.Books.Service;
using Tests.Utils;

namespace Tests.Services;

public sealed class BookServiceFacts : BaseUnitTest
{
    private readonly IBookService _bookService;

    public BookServiceFacts() : base()
    {
        _bookService = new BookService(BookRepository);
    }

    [Fact]
    public async Task CreateBook_WithEmptyFieldsThrowsArgumentNullException()
    {
        Core.Books.Book newBook = new()
        {
            Author = string.Empty,
            Category = string.Empty
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _bookService.AddBook(newBook)).ConfigureAwait(true);
    }

    [Fact]
    public async Task CreateBook_WithValidFieldsCreatesBook()
    {
        Core.Books.Book newBook = new()
        {
            Author = "DUMMY",
            Category = "DUMMY"
        };

        await _bookService.AddBook(newBook).ConfigureAwait(true);
        Core.Books.Book book = await BookRepository.AddBook(newBook).ConfigureAwait(true);

        Assert.NotNull(book);
        Assert.Equal("DUMMY", book.Author);
        Assert.Equal("DUMMY", book.Category);
    }
}