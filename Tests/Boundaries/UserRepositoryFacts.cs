using Core.User;
using Tests.Utils;

namespace Tests.Boundaries;

public sealed class UserRepositoryFacts : BaseUnitTest
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
    public async Task GetAllUsers_WithNoUsersReturnsEmptyList()
    {
        IEnumerable<User> users = await UserRepository.GetAllUsers().ConfigureAwait(true);

        Assert.NotNull(users);
        Assert.Empty(users);
    }
    
    [Fact]
    public async Task GetAllUsers_WithUsersReturnsListOfUsers()
    {
        await CreateUser("admin", "admin").ConfigureAwait(true);
        await CreateUser("user", "user").ConfigureAwait(true);
        
        IEnumerable<User> users = await UserRepository.GetAllUsers().ConfigureAwait(true);
        
        Assert.NotNull(users);
        Assert.NotEmpty(users);
        Assert.Equal(2, users.Count());
    }
    
    [Fact]
    public async Task GetUserByUsernameAndPassword_WithValidCredentialsReturnsUser()
    {
        await CreateUser("admin", "admin").ConfigureAwait(true);
        User userLoggingIn = new User()
        {
            Username = "admin",
            Password = "admin"
        };

        User result = await UserRepository.GetUserByUsernameAndPassword(userLoggingIn).ConfigureAwait(true);

        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
        Assert.Equal("admin", result.Password);
    }
}