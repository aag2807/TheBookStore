using Core.Boundaries.Persistance;
using Core.User.Aggregates;
using Triplex.Validations;

namespace Core.User.Service;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    async Task IUserService.CreateUser(User user)
    {
        Arguments.NotNull(user, nameof(user));
        Arguments.NotEmptyOrWhiteSpaceOnly(user.Username, nameof(user.Username));
        Arguments.NotEmptyOrWhiteSpaceOnly(user.Password, nameof(user.Password));
        Arguments.NotEmptyOrWhiteSpaceOnly(user.Email, nameof(user.Email));

        await _userRepository.AddUser(user).ConfigureAwait(true);
    }

    /// <inheritdoc />
    async Task<User> IUserService.LoginUser(LoginUser loginUser)
    {
        Arguments.NotNull(loginUser, nameof(loginUser));
        Arguments.NotEmptyOrWhiteSpaceOnly(loginUser.Username, nameof(loginUser.Username));
        Arguments.NotEmptyOrWhiteSpaceOnly(loginUser.Password, nameof(loginUser.Username));

        User coreUser = await _userRepository.GetUserByUsernameAndPassword(loginUser.ToCoreUser()).ConfigureAwait(true);
        Arguments.NotNull(coreUser, nameof(coreUser));

        return coreUser;
    }

    /// <inheritdoc />
    async Task<IEnumerable<User>> IUserService.GetAllUsers()
    {
        return await _userRepository.GetAllUsers().ConfigureAwait(true);
    }
}