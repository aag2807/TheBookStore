namespace Core.Books.Aggregates;

public sealed class AddBook
{
    public string? AuthorId { get; set; }

    public string Category { get; set; } = String.Empty;

    public Book ToBook()
    {
        return new Book
        {
            AuthorId = AuthorId,
            Category = Category
        };
    }
}