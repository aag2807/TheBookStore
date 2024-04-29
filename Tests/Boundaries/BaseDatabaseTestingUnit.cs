using Boundaries.Persistance.Context;
using Boundaries.Persistance.Repositories.User;
using Core.Boundaries.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Tests.Boundaries;

public class BaseDatabaseTestingUnit : IDisposable
{
    //repositories
    protected readonly IUserRepository UserRepository;

    private readonly BookDbContext _context;

    protected BaseDatabaseTestingUnit()
    {
        DbContextOptions<BookDbContext> options = new DbContextOptionsBuilder<BookDbContext>()
            .UseSqlServer("Data Source=localhost,1433;Initial Catalog=BookStore_test;User Id=sa;Password=yourStrong(!)Password;Encrypt=False;TrustServerCertificate=True;")
            .Options;

        _context = new BookDbContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();
        
        UserRepository = new UserRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}