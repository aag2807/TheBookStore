using Core.Books.Aggregates;
using Core.Boundaries.Persistance;
using Core.User.Aggregates;
using Triplex.Validations;

namespace Core.Books.Service;

public sealed class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    async Task IBookService.AddBook(Book book)
    {
        Arguments.NotNull(book, nameof(book));
        Arguments.NotEmptyOrWhiteSpaceOnly(book.AuthorId, nameof(book.AuthorId));
        Arguments.NotEmptyOrWhiteSpaceOnly(book.Category, nameof(book.Category));

        await _bookRepository.AddBook(book).ConfigureAwait(true);
    }
}