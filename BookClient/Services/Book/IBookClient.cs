namespace BookClient.Services.Book;

public interface IBookClient
{
    public Task<IEnumerable<Object>> GetBooksAsync();
}