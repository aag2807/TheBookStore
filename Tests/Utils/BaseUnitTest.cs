using AutoMapper;
using Boundaries.Persistance.Context;
using Boundaries.Persistance.Repositories.User;
using Core.Boundaries.Persistance;
using Core.User;
using Microsoft.EntityFrameworkCore;
using Shared.MappingProfiles.User;

namespace Tests.Utils;

public class BaseUnitTest : IDisposable
{
    private BookDbContext _context;
    private IMapper _mapper;

    //repositories
    protected IUserRepository UserRepository;

    protected BaseUnitTest()
    {
        ConfigureDatabase();
        ConfigureMapper();
        InstantiateRepositories();
    }

    private void ConfigureDatabase()
    {
        DbContextOptions<BookDbContext> options = new DbContextOptionsBuilder<BookDbContext>()
            .UseSqlServer("Data Source=localhost,1433;Initial Catalog=Book_Store_test;User Id=sa;Password=yourStrong(!)Password;Encrypt=False;TrustServerCertificate=True;")
            .Options;

        _context = new BookDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();
    }
    
    private void ConfigureMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UserProfile());
        });

        _mapper = mappingConfig.CreateMapper();
    }

    private void InstantiateRepositories()
    {
        UserRepository = new UserRepository(_context, _mapper);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    /// <summary>
    /// Creates a user and saves it to the repository
    /// </summary>
    /// <param name="username">A valid string primitive representing the users user</param>
    /// <param name="password">A valid string primitive representing the users Password</param>
    /// <param name="email">A valid string primitive representing the users Email</param>
    /// <param name="isAdmin">A boolean primitive representing the flag which indicates if the user is admin</param>
    /// <param name="isBlocked">A boolean primitive representing the flag which indicates if the user is blocked</param>
    /// <returns>A valid instance of <see cref="User"/></returns>
    protected async Task<User> CreateUser(string username, string password, string email = "", bool isAdmin = false, bool isBlocked = false)
    {
        User user = new User(username: username, password: password, email: string.IsNullOrWhiteSpace(email) ? $"{username}@{username}.com": email, isAdmin: isAdmin, isBlocked: isBlocked);
        await UserRepository.AddUser(user).ConfigureAwait(true);
        
        return user;
    }

    /// <summary>
    /// Clears the existing users in the migration to avoid conflicts
    /// </summary>
    protected void DeleteExistingUsers()
    {
        _context.Database.ExecuteSqlRaw("DELETE FROM [User];");
    }
}