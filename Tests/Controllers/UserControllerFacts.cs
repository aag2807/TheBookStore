using BookApi.Controllers;
using Core.User.Aggregates;
using Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Tests.Boundaries;
using Tests.Utils;

namespace Tests.Controllers;

public sealed class UserControllerFacts : BaseUnitTest
{
    private readonly UserController _userController;

    public UserControllerFacts() : base()
    {
        IUserService userService = new UserService(UserRepository);
        _userController = new UserController(userService);
    }

    [Fact]
    public async Task Login_WithEmptyFieldsThrowsArgumentNullException()
    {
        LoginUser loginUser = new LoginUser()
        {
            Username = string.Empty,
            Password = string.Empty,
        };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _userController.Login(loginUser).ConfigureAwait(true)).ConfigureAwait(true);
    }
    
    [Fact]
    public async Task Login_WithValidFieldsReturnsUser()
    {
        await CreateUser("dummy", "dummy", "dummy@dummy.com").ConfigureAwait(true);
        LoginUser loginUser = new LoginUser()
        {
            Username = "dummy",
            Password = "dummy",
        };
        
        IActionResult result = await _userController.Login(loginUser).ConfigureAwait(true);

        OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
        Core.User.User user = Assert.IsType<Core.User.User>(okObjectResult.Value);        
        Assert.NotNull(user);
        Assert.Equal("dummy", user.Username);
        Assert.Equal("dummy", user.Password);
    }
}