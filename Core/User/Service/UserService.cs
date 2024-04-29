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

    public async Task CreateUser(User user)
    {
    }

    public async Task<User> LoginUser(LoginUser loginUser)
    {
        Arguments.NotNull(loginUser, nameof(loginUser));
        Arguments.NotEmptyOrWhiteSpaceOnly(loginUser.Username, nameof(loginUser.Username));
        Arguments.NotEmptyOrWhiteSpaceOnly(loginUser.Password, nameof(loginUser.Username));

        User coreUser = await _userRepository.GetUserByUsernameAndPassword(loginUser.ToCoreUser()).ConfigureAwait(true);
        Arguments.NotNull(coreUser, nameof(coreUser));

        return coreUser;
    }
}