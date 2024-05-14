namespace Core.Books;

public sealed class Book
{
    public Book()
    {
    }

    public Book(string authorId, string category) {
        AuthorId = authorId;
        Category = category;
    }

    public int BookId { get; set;}

    public string? AuthorId {get; set;}

    public string Category {get; set;} = String.Empty;

}