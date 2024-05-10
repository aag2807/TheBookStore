namespace Core.Books;

public sealed class Book
{
    public Book()
    {
    }

    public Book(string author, string category) {
        Author = author;
        Category = category;
    }

    public int BookId { get; set;}

    public string Author {get; set;} = String.Empty;

    public string Category {get; set;} = String.Empty;

}