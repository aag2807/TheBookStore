using Boundaries.Persistance.Models.User;
using Core.User.Aggregates;
using Core.User.Service;
using Tests.Boundaries;

namespace Tests.Services;

public sealed class UserServiceFacts : BaseUnitTest
{
    private readonly IUserService _userService;

    public UserServiceFacts() : base()
    {
        _userService = new UserService(UserRepository);
    }

    [Fact]
    public async Task CreateUser_WithEmptyFieldsThrowsArgumentNullException()
    {
        Core.User.User newUser = new Core.User.User()
        {
            Username = string.Empty,
            Password = string.Empty,
            Email = string.Empty,
            IsAdmin = true,
            IsBlocked = false,
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _userService.CreateUser(newUser)).ConfigureAwait(true);
    }

    [Fact]
    public async Task CreateUser_WithValidFieldsCreatesUser()
    {
        Core.User.User newUser = new Core.User.User()
        {
            Username = "DUMMY",
            Password = "DUMMY",
            Email = "DUMMY@DUMMY.com",
            IsAdmin = false,
            IsBlocked = false,
        };
        
        await _userService.CreateUser(newUser).ConfigureAwait(true);
        Core.User.User user = await UserRepository.GetUserByUsernameAndPassword(newUser).ConfigureAwait(true);
        
        Assert.NotNull(user);
        Assert.Equal("DUMMY", user.Username);
        Assert.Equal("DUMMY", user.Password);
        Assert.Equal("DUMMY@DUMMY.com", user.Email);
    }
    
    [Fact]
    public async Task LoginUser_WithEmptyFieldsThrowsArgumentNullException()
    {
        LoginUser loginUser = new LoginUser()
        {
            Username = string.Empty,
            Password = string.Empty,
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _userService.LoginUser(loginUser)).ConfigureAwait(true);
    }

    [Fact]
    public async Task LoginUser_WithValidFieldsLogsInUser()
    {
        await CreateUser("dummy", "dummy", "dummy@dummy.com").ConfigureAwait(true);
        LoginUser loginUser = new LoginUser()
        {
            Username = "dummy",
            Password = "dummy",
        };
        
        Core.User.User user = await _userService.LoginUser(loginUser).ConfigureAwait(true);
        
        Assert.NotNull(user);
        Assert.Equal("dummy", user.Username);
        Assert.Equal("dummy", user.Password);
        Assert.Equal("dummy@dummy.com", user.Email);
    }
}