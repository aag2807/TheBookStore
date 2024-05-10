namespace Core.Books.Aggregates;

public sealed class AddBook
{
    public string Author { get; set; } = String.Empty;

    public string Category { get; set; } = String.Empty;

    public Book ToBook()
    {
        return new Book
        {
            Author = Author,
            Category = Category
        };
    }
}