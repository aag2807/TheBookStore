using AutoMapper;
using Boundaries.Persistance.Base;
using Boundaries.Persistance.Context;
using Core.Boundaries.Persistance;
using Core.Boundaries.Persistance.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Triplex.Validations;

namespace Boundaries.Persistance.Repositories.Book;

public sealed class BookRepository : BaseRepository<Boundaries.Persistance.Models.Book.Book, Core.Books.Book>, IBookRepository
{
    private readonly IBookDbContext _bookDbContext;

    public BookRepository(IBookDbContext context, IMapper mapper) : base(context, mapper)
    {
        _bookDbContext = context;
    }

    /// <inheritdoc />
    async Task<Core.Books.Book> IBookRepository.AddBook(Core.Books.Book book)
    {
        Arguments.NotNull(book, nameof(book));
        Persistance.Models.Book.Book dbBook = Models.Book.Book.FromCoreEntity(book);

        _bookDbContext.Book.Add(dbBook);

        await _bookDbContext.SaveChangesAsync().ConfigureAwait(true);

        return book;
    }

    /// <inheritdoc />
    async Task<Core.Books.Book> IBookRepository.GetBook(Core.Books.Book book)
    {
        Arguments.NotNull(book, nameof(book));
        Arguments.GreaterThan(book.BookId, 0, nameof(book.BookId));
        Arguments.NotEmptyOrWhiteSpaceOnly(book.AuthorId, nameof(book.AuthorId));
        Arguments.NotEmptyOrWhiteSpaceOnly(book.Category, nameof(book.Category));

        Models.Book.Book? dbBook = await _bookDbContext.Book
            .FirstOrDefaultAsync(dbBook => dbBook.BookId == book.BookId && dbBook.Author.ToString() == book.AuthorId && dbBook.Category == book.Category)
            .ConfigureAwait(true);

        Arguments.NotNull(dbBook, "Book not found");

        return dbBook!.ToCoreEntity();
    }
}