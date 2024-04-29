using Boundaries.Persistance.Context;
using Boundaries.Persistance.Models.Book;
using Boundaries.Persistance.Repositories.User;
using Core.Boundaries.Persistance;
using Core.User;
using Microsoft.EntityFrameworkCore;

namespace Tests.Boundaries;

public sealed class UserRepositoryFacts : IDisposable
{
    private readonly BookDbContext _context;
    private readonly IUserRepository _repository;

    public UserRepositoryFacts()
    {
        DbContextOptions<BookDbContext> options = new DbContextOptionsBuilder<BookDbContext>()
            .UseSqlServer("Data Source=localhost,1433;Initial Catalog=BookStore_test;User Id=sa;Password=yourStrong(!)Password;Encrypt=False;TrustServerCertificate=True;")
            .Options;

        _context = new BookDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();

        _repository = new UserRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task AddUser_WithValidUserAddsUserToDatabase()
    {
        var newUser = new User()
        {
            Username = "admin",
            Password = "admin",
            Email = "admin@email.com",
            IsAdmin = true,
            IsBlocked = false,
        };
        
        await _repository.AddUser(newUser).ConfigureAwait(true);
        IEnumerable<User> users = await _repository.GetAllUsers().ConfigureAwait(true);
        
        Assert.NotNull(users);
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task GetUserByUsernameAndPassword_WithValidCredentialsReturnsUser()
    {
        User newUser = new User()
        {
            Username = "admin",
            Password = "admin",
            Email = "admin@email.com",
            IsAdmin = true,
            IsBlocked = false,
        };
        
        User result = await _repository.GetUserByUsernameAndPassword(newUser).ConfigureAwait(true);

        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
        Assert.Equal("admin", result.Password);
    }
}