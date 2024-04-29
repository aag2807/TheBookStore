using Boundaries.Persistance.Context;
using Core.Boundaries.Persistance;
using Microsoft.EntityFrameworkCore;
using Triplex.Validations;

namespace Boundaries.Persistance.Repositories.User;

public sealed class UserRepository : IUserRepository
{
    private readonly IBookDbContext _bookDbContext;
    
    public UserRepository( IBookDbContext bookDbContext )
    {
        _bookDbContext = bookDbContext;
    }
    
    /// <inheritdoc />
    async Task<Core.User.User> IUserRepository.GetUserByUsernameAndPassword(Core.User.User user)
    {
        Arguments.NotNull(user, nameof(user));
        Arguments.NotEmptyOrWhiteSpaceOnly(user.Username, nameof(user.Username));
        Arguments.NotEmptyOrWhiteSpaceOnly(user.Password, nameof(user.Password));
        
        Models.User.User? dbUser = await _bookDbContext.User
            .FirstOrDefaultAsync(dbUser => dbUser.Username == user.Username && dbUser.Password == user.Password)
            .ConfigureAwait(true);
        
        Arguments.NotNull(dbUser, "Invalid credentials");

        return dbUser!.ToCoreEntity();
    }

    /// <inheritdoc />
    async Task IUserRepository.AddUser(Core.User.User user)
    {
        Arguments.NotNull(user, nameof(user));
        
        Persistance.Models.User.User dbUser = Models.User.User.FromCoreEntity(user);
        
        _bookDbContext.User.Add(dbUser);
        
        await _bookDbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    /// <inheritdoc />
    async Task<IEnumerable<Core.User.User>> IUserRepository.GetAllUsers()
    {
        IEnumerable<Core.User.User> users = await _bookDbContext.User
            .Select(dbUser => dbUser.ToCoreEntity())
            .ToListAsync();
        
        return await Task.FromResult(users).ConfigureAwait(true);
    }
}