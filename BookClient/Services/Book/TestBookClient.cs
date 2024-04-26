namespace BookClient.Services.Book;

public sealed class TestBookClient : IBookClient
{
    public async Task<IEnumerable<object>> GetBooksAsync()
    {
        return await Task.FromResult(Enumerable.Empty<object>()).ConfigureAwait(false);
    }
}