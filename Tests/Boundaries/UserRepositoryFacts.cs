using Core.User;

namespace Tests.Boundaries;

public sealed class UserRepositoryFacts : BaseDatabaseTestingUnit
{
    public UserRepositoryFacts() : base()
    {
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

        await UserRepository.AddUser(newUser).ConfigureAwait(true);
        IEnumerable<User> users = await UserRepository.GetAllUsers().ConfigureAwait(true);

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
        await UserRepository.AddUser(newUser).ConfigureAwait(true);

        User result = await UserRepository.GetUserByUsernameAndPassword(newUser).ConfigureAwait(true);

        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
        Assert.Equal("admin", result.Password);
    }
}