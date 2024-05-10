using AutoMapper;
using Boundaries.Persistance.Base;
using Boundaries.Persistance.Context;
using Core.Boundaries.Persistance;
using Core.Boundaries.Persistance.Util;
using Microsoft.EntityFrameworkCore;
using Triplex.Validations;

namespace Boundaries.Persistance.Repositories.User;

public sealed class UserRepository : BaseRepository<Boundaries.Persistance.Models.User.User, Core.User.User>, IUserRepository
{
    private readonly IBookDbContext _bookDbContext;

    public UserRepository(IBookDbContext context, IMapper mapper) : base(context, mapper)
    {
        _bookDbContext = context;
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

    /// <inheritdoc />
    async Task<Core.User.User> IUserRepository.GetUserByCriteria(Criteria criteria)
    {
        return await GetByCriteria(criteria).ConfigureAwait(true)!;
    }
    
    /// <inheritdoc />
    async Task<bool> IUserRepository.UserExists(Criteria criteria)
    {
        return await ExistsByCriteria(criteria).ConfigureAwait(true);
    }
}